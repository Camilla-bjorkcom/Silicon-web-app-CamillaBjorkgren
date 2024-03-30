﻿using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CoursesService(CoursesRepository coursesRepository)
{
    private readonly CoursesRepository _coursesRepository = coursesRepository;

    public async Task<IEnumerable<CourseEntity>> GetAllAsync()
    {
        try
        {
            var courses = await _coursesRepository.GetAllAsync();
            if (courses != null)
            {
                return courses;
            }
            return null!;
        }
        catch (Exception)
        {

            return null!;
        }
    }

    public async Task<CourseEntity> GetOneAsyncId(string id)
    {
        try
        {
            var course = await _coursesRepository.GetOneAsync(x => x.Id == id);
            if (course != null)
            {
                return course;
            }
            return null!;
        }
        catch (Exception) { return null!; }
    }
    public async Task<CourseEntity> GetOneAsyncTitle(string title)
    {
        try
        {
            var course = await _coursesRepository.GetOneAsync(x => x.Title == title);
            if (course != null)
            {
                return course;
            }
            return null!;
        }
        catch (Exception) { return null!; }
    }

    public async Task<bool> ExistsCourseAsync(string title)
    {
        try
        {
            var courseExisting = await _coursesRepository.Exists(x => x.Title == title);
            return courseExisting;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<bool> CreateCourseAsync(CourseEntity course)
    {
        try
        {
            var result = await _coursesRepository.CreateAsync(course);
            if (result != null)
            {
                return true;
            }
            return false;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<bool> UpdateCourseAsync(CourseEntity course)
    {
        try
        {
            var courseExisting = await _coursesRepository.GetOneAsync(x => x.Id == course.Id);
            if (courseExisting != null)
            {
                var result = await _coursesRepository.UpdateAsync(courseExisting);
                if (result != null)
                    return true;
                return false;

            }
            return false;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<bool> DeleteCourseAsync(CourseEntity course)
    {
        try
        {
            var result = await _coursesRepository.DeleteAsync(x => x.Title == course.Title);
            if (result)
                return true;
            return false;
        }                 
        catch (Exception)
        {

            return false!;
        }
    }
}
