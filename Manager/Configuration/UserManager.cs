using System;
using System.Security.Claims;
using payday_server.Layers.ContextLayer;
using payday_server.Model;
using payday_server.Shared;
using Microsoft.EntityFrameworkCore;

namespace payday_server.Manager.Configuration
{
    public class UserManager : IManager
    {
        private readonly AppDBContext _context;
        public UserManager(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetDataAsync( ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse ();
            try {

                
                var _Table = await _context.Users
                .Include(r => r.UserRole)
                .Where (a => a.Action != Enums.Operations.D.ToString ()).OrderBy (o => o.NormalizedName).ToListAsync ();

                if (_Table == null || _Table.Count == 0) {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();;
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                apiResponse.data = _Table;
                return apiResponse;
            }
            catch (Exception e) {
                string innerexp = e.InnerException == null? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }

        public async Task<ApiResponse> GetDataByIdAsync(Guid _Id,  ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse ();
            try {
                
                var _Table = await _context.Users
                .Include(r => r.UserRole)
                .Where (a => a.Id == _Id && a.Action != Enums.Operations.D.ToString ()).FirstOrDefaultAsync ();

                if (_Table == null ) {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                apiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                apiResponse.data = _Table;
                return apiResponse;
            } 
            catch (Exception e) {
                string innerexp = e.InnerException == null? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }

        public async Task<ApiResponse> AddAsync(object model,  ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse ();
            try {

                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (User) model;
                string error = "";
                bool _EmailExists = _context.Users.Any (rec => rec.Email.Trim().ToLower().Equals(_model.Email.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());
                bool _ContactExists = _context.Users.Any (rec => rec.Contact.Trim().ToLower().Equals(_model.Contact.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());

                if (_EmailExists) {
                    error = error + "Email";
                }

                if (_ContactExists) 
                {
                    error = error + "Phone Number";
                }

                if (_EmailExists || _ContactExists) {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
                    apiResponse.message = error + " Already Exists";
                    return apiResponse;
                }

                _model.UserIdInsert = Guid.Parse(_UserId);
                _model.InsertDate = DateTime.Now;
                _model.Action = Enums.Operations.A.ToString();

                await _context.Users.AddAsync (_model);
                _context.SaveChanges ();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                apiResponse.message =  _model.NormalizedName + " has been added successfully";
                return apiResponse;

            } catch (DbUpdateException _exceptionDb) {

                string innerexp = _exceptionDb.InnerException == null? _exceptionDb.Message : _exceptionDb.Message + " Inner Error : " + _exceptionDb.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;

            } catch (Exception e) {

                string innerexp = e.InnerException == null? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }
        public async Task<ApiResponse> UpdateAsync(object model,  ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse ();
            try {

                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (User) model;
                string error = "";
                bool _EmailExists = _context.Users.Any(rec => rec.Email.Trim().ToLower().Equals(_model.Email.Trim().ToLower()) && rec.Id != _model.Id && rec.Action != Enums.Operations.D.ToString ());
                bool _ContactExists = _context.Users.Any(rec => rec.Contact.Trim().ToLower().Equals(_model.Contact.Trim().ToLower()) && rec.Id != _model.Id && rec.Action != Enums.Operations.D.ToString ());

                if (_EmailExists) {
                    error = error + "Email";
                }

                if (_ContactExists) 
                {
                    error = error + "Phone Number";
                }

                if (_EmailExists || _ContactExists) {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
                    apiResponse.message = error + " Already Exists";
                    return apiResponse;
                }
                var result = _context.Users.Where (a => a.Id == _model.Id && a.Action != Enums.Operations.D.ToString ()).FirstOrDefault ();
                if (result == null) {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }

                result.Code = _model.Code;
                result.FirstName = _model.FirstName;
                result.LastName =  _model.LastName ;
                result.NormalizedName = _model.FirstName + " " + _model.LastName ;
                result.Contact = _model.Contact;
                result.CNIC = _model.CNIC;
                result.Email = _model.Email;
                result.PermitForm = _model.PermitForm;
                result.PermitTo = _model.PermitTo;
                result.HashPassword = _model.HashPassword;
                result.RoleId = _model.RoleId;                
                result.TenantsCheck = _model.TenantsCheck;                
                result.Type = _model.Type;                
                result.Active = _model.Active;
                result.UserIdUpdate = Guid.Parse(_UserId);
                result.Action = Enums.Operations.E.ToString ();
                result.UpdateDate = DateTime.Now;

                await _context.SaveChangesAsync();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                apiResponse.message = result.NormalizedName + " has been updated successfully";
                return apiResponse;

            } catch (DbUpdateException _exceptionDb) {

                string innerexp = _exceptionDb.InnerException == null? _exceptionDb.Message : _exceptionDb.Message + " Inner Error : " + _exceptionDb.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;

            } catch (Exception e) {
                string innerexp = e.InnerException == null? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }

        public async Task<ApiResponse> DeleteAsync(Guid _Id,  ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse ();
            try {
                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var result = _context.Users.Where (a => a.Id == _Id && a.Action != Enums.Operations.D.ToString ()).FirstOrDefault ();
                if (result == null) {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }

                result.UserIdDelete = Guid.Parse(_UserId);
                result.Action = Enums.Operations.D.ToString ();
                result.DeleteDate = DateTime.Now;

                await _context.SaveChangesAsync();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                apiResponse.message =  result.NormalizedName + " has been deleted successfully";
                return apiResponse;

            } catch (DbUpdateException _exceptionDb) {

                string innerexp = _exceptionDb.InnerException == null? _exceptionDb.Message : _exceptionDb.Message + " Inner Error : " + _exceptionDb.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;

            } catch (Exception e) {

                string innerexp = e.InnerException == null? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString ();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }
    }
}