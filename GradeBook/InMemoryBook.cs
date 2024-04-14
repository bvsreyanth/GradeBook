using GradeBook;

namespace GradeBook
{
    public class NameObject
    {
        public NameObject(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NameObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();

    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name) { }

        public override event GradeAddedDelegate GradeAdded;

        // This method is storing all the grades that have been entered to a text document.
        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {

                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs()); // Raising the event 
                }
            }

        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null && line != "")
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }

}
public delegate void GradeAddedDelegate(object sender, EventArgs e);
public class InMemoryBook : Book
{
    public override event GradeAddedDelegate GradeAdded;

    private List<double> grades;

    /*readonly int a = 1;*/ // while using readonly we can declare the variable in the sameline or in the constructor  
    /* const string CATEGORY = "science"; */// the value of const can't be changed and the all the alphabets should be in capital letters.
    public InMemoryBook(string name) : base(name)
    {
        /*a++;*/


        grades = new List<double>();
        Name = name;

    }

    // This method is taking values which are of type char.
    public void AddGrade(char letter)
    {
        switch (letter)
        {
            case 'A':
                AddGrade(90);
                break;

            case 'B':
                AddGrade(80);
                break;

            case 'C':
                AddGrade(70);
                break;

            default:
                break;
        }
    }

    // This is a overridden method which is a good example of Runtime polymorphism
    public override void AddGrade(double grade)
    {
        if (grade <= 100 && grade >= 0)
        {
            grades.Add(grade);

            if (GradeAdded != null)
            {
                GradeAdded(this, new EventArgs());
            }

        }
        else
        {
            throw new ArgumentException($"Invalid {nameof(grade)}");
        }
    }



    public override Statistics GetStatistics()
    {

        Statistics s1 = new();


        for (var index = 0; index < grades.Count; index++)
        {
            s1.Add(grades[index]);

            /*if (grades[index] == 65.2)
            {
                break;
            }*/

            /*if (grades[index] == 65.2)
            {
                continue;
            }*/

            /* if (grades[index] == 65.2)
             {
                 goto over;
             }*/


        }
        /* over:*/
        return s1;

    }
}

