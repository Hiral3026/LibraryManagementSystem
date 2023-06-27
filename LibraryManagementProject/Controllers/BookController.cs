using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using LibraryManagementProject.Models;
using System.Web.Script.Serialization;

namespace LibraryManagementProject.Controllers
{
    public class BookController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static BookController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44380/api/BookData/");
        }
        // GET: Book/List
        public ActionResult List()
        {
            
            string url = "listbooks";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<BookDto> books = response.Content.ReadAsAsync<IEnumerable<BookDto>>().Result;
            Debug.WriteLine("Number of books: ");
            Debug.WriteLine(books.Count());

            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            
            string url = "FindBook/" +id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            BookDto selectedbooks = response.Content.ReadAsAsync<BookDto>().Result;
            Debug.WriteLine("Animal Received ");
            Debug.WriteLine(selectedbooks.Title);

            return View(selectedbooks);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Book/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book Book)
        {
            Debug.WriteLine("The inputted book is: ");
            Debug.WriteLine(Book.Title);
            string url = "addbook";

            string jsonpayload = jss.Serialize(Book);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
