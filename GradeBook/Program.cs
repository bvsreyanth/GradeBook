using GradeBook;

internal class Program
{
    private static void Main(string[] args)
    {
        /* Creating an Object of Book */
        IBook book = new DiskBook("Sreyanth Grade Book");

        book.GradeAdded += OnGradeAdded;

        GradeAddition(book);


        /* Adding all the grades to list */
        /*book.AddGrade(56.10);
        book.AddGrade(89.1);
        book.AddGrade(90.5);
        book.AddGrade(77.5);*/

        /* invoking ShowStatistics method */

        var stats = book.GetStatistics();

        Console.WriteLine(book.Name);
        Console.WriteLine($"The average is {stats.Average:N2}");
        Console.WriteLine($"The high score is {stats.High}");
        Console.WriteLine($"The Low score is {stats.Low}");
        Console.WriteLine($"The grade is {stats.Letter}");

    }

    private static void GradeAddition(IBook book)
    {
        while (true)
        {
            Console.WriteLine("Enter a grade or q to quit!!");
            var grade = Console.ReadLine();

            if (grade == "q" || grade == "Q")
            {
                break;
            }

            try
            {
                book.AddGrade(double.Parse(grade));

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                /*Console.WriteLine("You are in Finally block");*/
            }

        }
    }

    static void OnGradeAdded(object sender, EventArgs e)
    {

        Console.WriteLine("A Grade was Added");
    }

}
