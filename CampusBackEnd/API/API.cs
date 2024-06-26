﻿using CampusBackEnd.Interfaces;
using CampusBackEnd.Repository;
using CampusBackEnd.DataStorage;

namespace CampusBackEnd.API
{
    /// <summary>
    /// Exposes public methods that can be called by the FrontEnd project.
    /// Link between backend (database management and operations) and frontend (UI).
    /// </summary>
    public class API
    {
        private ICampusRepository _campusRepository;

        public API()
        {
            this._campusRepository = new CampusRepository();
        }

        // STUDENTS
        public List<Student> GetStudents()
        {
            return this._campusRepository.GetStudents();
        }

        public Student GetStudent(int ID)
        {
            return this._campusRepository.GetStudent(ID);
        }

        public Student AddNewGrade(int courseID, double grade, string comment, Student student)
        {
            return this._campusRepository.AddNewGrade(courseID, grade, comment, student);
        }

        public double CalculateCourseAverage(List<Grade> courseGrades)
        {
            return this._campusRepository.CalculateCourseAverage(courseGrades);
        }

        public double CalculateGeneralAverage(Student student)
        {
            return this._campusRepository.CalculateGeneralAverage(student);
        }

        // COURSES
        public List<Course> GetCourses()
        {
            return this._campusRepository.GetCourses();
        }

        public Course GetCourse(int ID)
        {
            return this._campusRepository.GetCourse(ID);
        }

        public void RemoveCourse(Course course)
        {
            this._campusRepository.RemoveCourse(course);
        }

        public void AddNewItem(string firstName, string lastName, DateTime birthDate)
        {
            this._campusRepository.AddNewItem(firstName, lastName, birthDate);
        }

        public Course AddNewItem(string fieldName)
        {
            return this._campusRepository.AddNewItem(fieldName);
        }
    }
}
