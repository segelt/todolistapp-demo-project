using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListApp.Domain;

namespace TodoListApp.Infrastructure.Data.Config
{
    public class TodoTaskConfiguration : IEntityTypeConfiguration<TodoTask>
    {
        public void Configure(EntityTypeBuilder<TodoTask> builder)
        {
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Completed).HasDefaultValue(false);
        }
    }
}
