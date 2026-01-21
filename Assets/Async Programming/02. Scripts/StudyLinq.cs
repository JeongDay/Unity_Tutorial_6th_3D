using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Student
{
    public int studentID;
    public string studentName;

    public Student(int studentID, string studentName)
    {
        this.studentID = studentID;
        this.studentName = studentName;
    }
}

[Serializable]
public class Grade
{
    public int studentID;
    public string subject;
    public int score;

    public Grade(int studentID, string subject, int score)
    {
        this.studentID = studentID;
        this.subject = subject;
        this.score = score;
    }
}

public class StudyLinq : MonoBehaviour
{
    public List<Student> students = new List<Student>();
    public List<Grade> grades = new List<Grade>();

    void Awake()
    {
        students.Add(new Student(1, "Alice"));
        students.Add(new Student(2, "Bob"));
        students.Add(new Student(3, "Charlie"));
        students.Add(new Student(4, "Eve"));

        grades.Add(new Grade(1, "Math", 90));
        grades.Add(new Grade(2, "Science", 85));
        // grades.Add(new Grade(3, "English", 92));
        // grades.Add(new Grade(4, "Math", 78));
    }

    void Start()
    {
        // var innerJoin = from student in students
        //                 join grade in grades on student.studentID equals grade.studentID
        //                 select new
        //                 {
        //                     StudentID = student.studentID,
        //                     StudentName = student.studentName,
        //                     Subject = grade.subject,
        //                     Score = grade.score
        //                 };

        var outerJoin = from student in students
                        join grade in grades on student.studentID equals grade.studentID into studentGrades
                        from studentGrade in studentGrades.DefaultIfEmpty()
                        select new
                        {
                            StudentID = student.studentID,
                            StudentName = student.studentName,
                            Subject = studentGrade?.subject ?? "None", // subject가 null이면 null로 적용
                            Score = studentGrade?.score ?? 0 // score가 null이면 0으로 적용
                        };

        foreach (var item in outerJoin)
            Debug.Log($"{item.StudentID}, {item.StudentName}, {item.Subject}, {item.Score}");
    }
}