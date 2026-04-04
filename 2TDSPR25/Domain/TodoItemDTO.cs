using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _2TDSPR25.Domain
{
    [Serializable]
    public class TodoItemDTO
    {
        [property:Description("Id da tarefa")]
        public int Id { get; set; }
        [property:Description("Nome da tarefa")]
        [property:MinLength(3)]
        [property:MaxLength(255)]
        [property:DefaultValue("Tarefa sem nome")]
        public string? Name { get; set; }
        [property:Description("Status de conclusão da tarefa")]
        public bool IsComplete { get; set; }
        [property:Description("Dia da semana para completar a tarefa")]
        public DayOfWeekAsString dayOfWeekToComplete { get; set; }

        public TodoItemDTO() { }
        public TodoItemDTO(Todo todoItem) => (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
    }
}
