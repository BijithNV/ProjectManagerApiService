using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerEntities
{
    [Serializable]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Priority { get; set; }
        public bool IsParentTask { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public Task Parent { get; set; }

        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
