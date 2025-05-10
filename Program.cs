using NotesApp.Model;
using NotesApp.NotesAppContext;

using var db = new NoteAppDbContext();
db.Database.EnsureCreated();

while (true)
{
    Console.WriteLine("\n📝 Notes App");
    Console.WriteLine("1. Add Note");
    Console.WriteLine("2. View Notes");
    Console.WriteLine("3. Delete Note");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");
    string input = Console.ReadLine();

    if (input == "1") AddNote(db);
    else if (input == "2") ViewNote(db);
    else if (input == "3") DeleteNote(db);
    else if (input == "4") break;
}

static void AddNote(NoteAppDbContext db)
{
    Console.WriteLine("Title: ");
    string enterTitle = Console.ReadLine();
    Console.WriteLine("Content: ");
    string content = Console.ReadLine();

    var note = new Note { Title = enterTitle, Content = content };
    db.Notes.Add(note);
    db.SaveChanges();
    Console.WriteLine("✅ Note Added!");

    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    string folderPath = Path.Combine(desktopPath, "MyNotes");
    Directory.CreateDirectory(folderPath);

    string saveNote = Path.Combine(folderPath, "SaveNoteStorage.txt");

    string textToSave = $"[{DateTime.Now:g}] {enterTitle}\n{content}\n---\n";
    File.AppendAllText(saveNote, textToSave);

    Console.WriteLine($"💾 Note saved in: {saveNote}");

}

static void ViewNote(NoteAppDbContext db)
{
    var notes = db.Notes.OrderByDescending(e => e.CreatedAt).ToList();


    if (!notes.Any())
    {
        Console.WriteLine("🚫 Notes not found");
        return;
    }

    foreach (var note in notes)
    {
        Console.WriteLine($"\n🗒️ {note.Id} - {note.Title} ({note.CreatedAt:g}])");
        Console.WriteLine(note.Content);
    }
}

static void DeleteNote(NoteAppDbContext db)
{
    Console.WriteLine("Enter Note ID to delete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var note = db.Notes.Find(id);
        if (note != null)
        {
            db.Notes.Remove(note);
            db.SaveChanges();
            Console.WriteLine("🗑️ Note Removed");
        }
        else
        {
            Console.WriteLine("❌ Note not found");
        }
    }
}
