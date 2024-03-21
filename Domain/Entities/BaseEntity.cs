
namespace Domain.Entities
{
    // Represents the base class for entities
    public abstract partial class BaseEntity
	{
        /// <summary>
        /// Identifier for each table
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Insert date for entries 
        /// </summary>
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// Update date for entries
        /// </summary>
        public DateTime UpdateDate { get; set; }

    }
}

