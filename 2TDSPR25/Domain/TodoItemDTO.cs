using Microsoft.AspNetCore.Http.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

namespace _2TDSPR25.Domain
{
    [Serializable]
    public class TodoItemDTO
    
        : IEndpointParameterMetadataProvider, IBindableFromHttpContext<TodoItemDTO>
        {
            public static void PopulateMetadata(
                ParameterInfo parameter,
                EndpointBuilder builder)
            {
                builder.Metadata.Add(
                    new AcceptsMetadata(
                        new[] { "application/json", "text/json", "application/xml", "text/xml" },
                        typeof(TodoItemDTO)
                    )
                );
            }

            public static async ValueTask<TodoItemDTO?> BindAsync(
                HttpContext context,
                ParameterInfo parameter)
            {
                var contentType = context.Request.ContentType?.Split(';')[0].Trim().ToLowerInvariant();

                // Handle JSON
                if (contentType is "application/json" or "text/json" or null)
                {
                    try
                    {
                        var todo = await JsonSerializer.DeserializeAsync<Todo>(context.Request.Body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }, context.RequestAborted);

                        if (todo is null) return null;
                        return new TodoItemDTO(todo);
                    }
                    catch
                    {
                        return null;
                    }
                }

                // Handle XML
                if (contentType is "application/xml" or "text/xml")
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(Todo));
                        if (serializer.Deserialize(context.Request.Body) is Todo todo)
                        {
                            return new TodoItemDTO(todo);
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }

                return null;
            }

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
