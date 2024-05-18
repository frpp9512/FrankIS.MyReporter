using SQLite;

namespace FrankIS.MyReporter.MobileClient.Models;

public record SqliteEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
