using System.ComponentModel.DataAnnotations;

namespace ESA.Views.StudentMaster
{

    public class CourseBaseModel
    {
        public int Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string CourseID { get; set; }
        public bool Active { get; set; }

    }

    public class CourseViewModel : CourseBaseModel
    {
        public Guid Id { get; set; }
        public int LastCode { get; set; }
    }

    public class CourseViewByIdModel : CourseBaseModel
    {
        public Guid Id { get; set; }
    }

    public class CourseAddModel : CourseBaseModel
    {
        public Guid MenuId { get; set; }
    }

    public class CourseUpdateModel : CourseBaseModel
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }

    }

    public class CourseDeleteModel : CourseBaseModel
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }

    }
}