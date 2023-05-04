using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CattleMgm.Data.Entities;
using Microsoft.Reporting.NETCore;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Text.Json;
using System.IO;
using CattleMgm.Data;
using CattleMgm.Models;
using Microsoft.AspNetCore.Http;
using CattleMgm.Repository.Cattles;
using CattleMgm.Repository.Farm;
using Microsoft.AspNetCore.Hosting;
using CattleMgm.Models.Session;

namespace CattleMgm.Controllers
{
    public class Reports : BaseController
    {
        private IWebHostEnvironment _webHostEnvironment;

        public Reports(ApplicationDbContext context, praktikadbContext db,
          UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment) : base(context, db, userManager)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Design(string format, bool landscape = false)
        {
            string filepath = Path.Join(_webHostEnvironment.ContentRootPath, HttpContext.Session.GetString("Path"));
            using (var filestram = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            { 
                string json = System.Text.Json.JsonSerializer.Serialize(HttpContext.Session.Get<List<object>>("queryresult"));

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);

                LocalReport report = new LocalReport();
                report.LoadReportDefinition(filestram);
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                #region width and height per report
                double page_width = 8.5;
                double page_height = 11;
                if (landscape)
                {
                    page_height = 8.5;
                    page_width = 11;
                }
                switch (HttpContext.Session.GetString("Path"))
                {
                    case "Reports\\OutcomeOrderGeneral.rdl":
                        {
                            page_width = 14;
                            var parameter1 = new ReportParameter("Param1", HttpContext.Session.GetString("Param1"));
                            var parameter2 = new ReportParameter("Param2", HttpContext.Session.GetString("Param2"));
                            report.SetParameters(parameter1);
                            report.SetParameters(parameter2);
                        }
                      
                        break;
                    case "Reports\\JournalsIndex.rdl":
                        page_width = 14;
                        break;
                }
                #endregion
                string deviceInfo = "<DeviceInfo>" + "  <OutputFormat>PDF</OutputFormat>" + "  <PageWidth>" + page_width + "in</PageWidth>" + "  <PageHeight>" + 11 + "</PageHeight>" + "  <MarginTop>0.5in</MarginTop>" + "  <MarginLeft>0in</MarginLeft>" + "  <MarginRight>0in</MarginRight>" + "  <MarginBottom>0.5in</MarginBottom>" + "</DeviceInfo>";
             
                 byte[] reportformat = report.Render(format: format, deviceInfo);
                string filename = string.Format("{0}.{1}", "Raporti", "pdf");
                string contenttype = "application/pdf";

                if (format == "excel")
                {
                    contenttype = "application/ms-excel";
                    filename = string.Format("{0}.{1}", "Raporti", "xls");
                    return File(reportformat, contentType: Response.ContentType = contenttype, filename);

                }
                return File(reportformat, contentType: Response.ContentType = contenttype);
            }
        } 

    }
}

