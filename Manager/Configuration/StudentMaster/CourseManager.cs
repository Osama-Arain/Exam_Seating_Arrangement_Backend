using System;
using System.Security.Claims;
using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Shared;
using Microsoft.EntityFrameworkCore;

namespace ESA.Manager.Configuration
{
    public class CourseManager : IManager
    {
        private readonly AppDBContext _context;
        public CourseManager(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.Courses.Where(a => a.Action != Enums.Operations.D.ToString()).OrderBy(o => o.Code).ToListAsync();

                if (_Table == null || _Table.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); ;
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _Table;
                return apiResponse;
            }
            catch (Exception e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }

        public async Task<ApiResponse> GetDataByIdAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.Courses
                .Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefaultAsync();

                if (_Table == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _Table;
                return apiResponse;
            }
            catch (Exception e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }

        public async Task<ApiResponse> AddAsync(object model, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (Course)model;
                string error = "";

                bool _CourseNameExist = _context.Courses.Any(rec => rec.Name.Trim().ToLower().Equals(_model.Name.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());
                bool _CourseIDExist = _context.Courses.Any(rec => rec.CourseID.Trim().ToLower().Equals(_model.CourseID.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());

                if (_CourseNameExist)
                {
                    error = error + " Course Name ";
                }
                if (_CourseIDExist)
                {
                    error = error + " Course ID ";
                }
                if (_CourseNameExist || _CourseIDExist)
                {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    apiResponse.message = error + " Already Exists";
                    return apiResponse;
                }

                var lastCode = _context.Courses.Where(x=>x.Action!= Enums.Operations.D.ToString())
                    .OrderByDescending(c => c.Code)
                    .Select(c => c.Code)
                    .FirstOrDefault();

                _model.UserIdInsert = Guid.Parse(_UserId);
                _model.InsertDate = DateTime.Now;
                _model.Action = Enums.Operations.A.ToString();
                _model.Code = lastCode + 1;

                await _context.Courses.AddAsync(_model);
                _context.SaveChanges();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = _model.Name + " has been added successfully";
                return apiResponse;

            }
            catch (DbUpdateException _exceptionDb)
            {

                string innerexp = _exceptionDb.InnerException == null ? _exceptionDb.Message : _exceptionDb.Message + " Inner Error : " + _exceptionDb.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }
        public async Task<ApiResponse> UpdateAsync(object model, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (Course)model;
                string error = "";

                bool _CourseNameExist = _context.Courses.Any(rec => rec.Name.Trim().ToLower().Equals(_model.Name.Trim().ToLower()) && rec.Id != _model.Id && rec.Action != Enums.Operations.D.ToString());
                bool _CourseIDExist = _context.Courses.Any(rec => rec.CourseID.Trim().ToLower().Equals(_model.CourseID.Trim().ToLower()) && rec.Id != _model.Id && rec.Action != Enums.Operations.D.ToString());

                if (_CourseNameExist)
                {
                    error = error + " Course Name ";
                }
                if (_CourseIDExist)
                {
                    error = error + " Course ID ";
                }
                if (_CourseNameExist || _CourseIDExist)
                {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    apiResponse.message = error + " Already Exists";
                    return apiResponse;
                }

                
                var result = _context.Courses.Where(a => a.Id == _model.Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (result == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }

                result.Code = _model.Code;
                result.Name = _model.Name;
                result.CourseID = _model.CourseID;
                result.Active = _model.Active;
                result.UserIdUpdate = Guid.Parse(_UserId);
                result.Action = Enums.Operations.E.ToString();
                result.UpdateDate = DateTime.Now;

                await _context.SaveChangesAsync();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = result.Name + " has been updated successfully";
                return apiResponse;

            }
            catch (DbUpdateException _exceptionDb)
            {

                string innerexp = _exceptionDb.InnerException == null ? _exceptionDb.Message : _exceptionDb.Message + " Inner Error : " + _exceptionDb.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;

            }
            catch (Exception e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }

        public async Task<ApiResponse> DeleteAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var result = _context.Courses.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (result == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }

                result.UserIdDelete = Guid.Parse(_UserId);
                result.Action = Enums.Operations.D.ToString();
                result.DeleteDate = DateTime.Now;

                await _context.SaveChangesAsync();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = result.Name + " has been deleted successfully";
                return apiResponse;

            }
            catch (DbUpdateException _exceptionDb)
            {

                string innerexp = _exceptionDb.InnerException == null ? _exceptionDb.Message : _exceptionDb.Message + " Inner Error : " + _exceptionDb.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = innerexp;
                return apiResponse;
            }
        }
    }
}