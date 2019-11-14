using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

namespace StudentExercisesMVC.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IConfiguration _config;

        public ExercisesController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Exercises
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT e.Id, e.ExerciseName, e.ExerciseLanguage
                                 FROM Exercise e
                                         ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            Language = reader.GetString(reader.GetOrdinal("ExerciseLanguage"))
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return View(exercises);
                }
            }
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT e.Id, e.ExerciseName, e.ExerciseLanguage
                                 FROM Exercise e
                                    WHERE @id = e.id
                                         ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = new Exercise();
                    while (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            Language = reader.GetString(reader.GetOrdinal("ExerciseLanguage"))
                        };

                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exercise exercise)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Exercise (ExerciseName, ExerciseLanguage) Values (@exerciseName, @exerciseLanguage)";
                        cmd.Parameters.Add(new SqlParameter("@exerciseName", exercise.Name));
                        cmd.Parameters.Add(new SqlParameter("@exerciseLanguage", exercise.Language));
                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT e.Id, e.ExerciseName, e.ExerciseLanguage
                                 FROM Exercise e
                                    WHERE @id = e.id
                                         ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = new Exercise();
                    while (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            Language = reader.GetString(reader.GetOrdinal("ExerciseLanguage"))
                        };

                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Exercise exercise)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Update Exercise set ExerciseName = @exerciseName, ExerciseLanguage = @exerciseLanguage where id = @id";
                        cmd.Parameters.Add(new SqlParameter("@exerciseName", exercise.Name));
                        cmd.Parameters.Add(new SqlParameter("@exerciseLanguage", exercise.Language));
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.ExecuteNonQuery();
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT e.Id, e.ExerciseName, e.ExerciseLanguage
                                 FROM Exercise e
                                    WHERE @id = e.id
                                         ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = new Exercise();
                    while (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("ExerciseName")),
                            Language = reader.GetString(reader.GetOrdinal("ExerciseLanguage"))
                        };

                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfrimed(int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Delete from Exercise where id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.ExecuteNonQuery();
                    }
                }



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}