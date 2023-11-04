using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace student_grade
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StudentList.stud = new List<Student>();
        }

        private bool isValidGrade(int grade)
        {
            return grade >= 2 && grade <= 5;
        }

        private void loadStudentFile(string filename)
        {
            try
            { 
            string[] lines = File.ReadAllLines(filename, Encoding.Default);
            List<Student> students = new List<Student>();

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');

                    int MathGrade = Convert.ToInt32(data[3]);
                    int InfoGrade = Convert.ToInt32(data[4]);
                    int PhysGrade = Convert.ToInt32(data[5]);

                    if (isValidGrade(MathGrade) && isValidGrade(InfoGrade) && isValidGrade(PhysGrade))
                    {
                        Student student = new Student()
                        {
                            Name = data[0],
                            LName = data[1],
                            PName = data[2],
                            Math = MathGrade,
                            Info = InfoGrade,
                            Phys = PhysGrade
                        };
                        students.Add(student);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                StudentList.stud = students;
                StudentListDG.ItemsSource = StudentList.stud;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в файле");
            }
        }

        private void saveStudentFile(string filename)
        {
            StringBuilder sb = new StringBuilder();
            if (StudentListDG != null)
            {
                foreach (var student in StudentListDG.ItemsSource)
                {
                    Student s = student as Student;
                    sb.AppendLine($"{s.Name},{s.LName},{s.PName},{s.Math},{s.Info},{s.Phys}");
                }
                File.WriteAllText(filename, sb.ToString(), Encoding.Default);
            }
        }

        private void loadStudent_Click(object sender, RoutedEventArgs e)
        {
            loadStudentFile("studentlist.txt");
        }

        private void saveStudent_Click(object sender, RoutedEventArgs e)
        {
            saveStudentFile("studentlist.txt");
        }

        private void addStudentButton_Click(object sender, RoutedEventArgs e)
        {
            int n;
            if (!string.IsNullOrWhiteSpace(NameS.Text) &&
                !string.IsNullOrWhiteSpace(LastNameS.Text) &&
                !string.IsNullOrWhiteSpace(PatronymeS.Text) &&
                int.TryParse(MathGrade.Text, out n) && n >= 2 && n <= 5 &&
                int.TryParse(InfoGrade.Text, out n) && n >= 2 && n <= 5 &&
                int.TryParse(PhysGrade.Text, out n) && n >= 2 && n <= 5)
            {
                Student student = new Student()
                {
                    Name = NameS.Text,
                    LName = LastNameS.Text,
                    PName = PatronymeS.Text,
                    Math = Convert.ToInt32(MathGrade.Text),
                    Info = Convert.ToInt32(InfoGrade.Text),
                    Phys = Convert.ToInt32(PhysGrade.Text)
                };
                StudentList.stud.Add(student);
                StudentListDG.ItemsSource = null;
                StudentListDG.ItemsSource = StudentList.stud;
            }
            else
            {
                MessageBox.Show("Ошибка добавления");
            }
            NameS.Text = null;
            LastNameS.Text = null;
            PatronymeS.Text = null;
            MathGrade.Text = null;
            InfoGrade.Text = null;
            PhysGrade.Text = null;
        }
    }
}
