using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public StudentiController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select StudentiId, EmriStudentit, Mbiemri, Ditelindja, Department, 
                Kontakti, NrPersonal, convert(varchar(10),DataRegjistrimit,120) as DataRegjistrimit,
                LLojiRegjistrimit
                from dbo.Studenti
                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Studenti stu)
        {
            string query = @"
                        insert into dbo.Studenti 
                    (EmriStudentit,Mbiemri,Ditelindja,Department,Kontakti,NrPersonal,DataRegjistrimit,LLojiRegjistrimit)
                    values
                    (
                    '" + stu.EmriStudentit + @"'
                    ,'" + stu.Mbiemri + @"'
                    ,'" + stu.Ditelindja + @"'
                    ,'" + stu.Department + @"'
                    ,'" + stu.Kontakti + @"'
                    ,'" + stu.NrPersonal + @"'
                    , '" + stu.DataRegjistrimit + @"'
                    ,'" + stu.LLojiRegjistrimit + @"'
                     )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("U shtua me sukses");

        }

        [HttpPut]
        public JsonResult Put(Studenti stu)
        {
            string query = @"
                        update dbo.Studenti set 
                EmriStudentit ='" + stu.EmriStudentit + @"'
                ,Mbiemri ='" + stu.Mbiemri + @"'
                ,Ditelindja ='" + stu.Ditelindja + @"'
                ,Department ='" + stu.Department + @"'
                ,Kontakti ='" + stu.Kontakti + @"'
                ,NrPersonal ='" + stu.NrPersonal + @"'
                ,DataRegjistrimit ='" + stu.DataRegjistrimit + @"'
                ,LLojiRegjistrimit ='" + stu.LLojiRegjistrimit + @"'
                   where StudentiId = " + stu.StudentiId + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("U perditesua me sukses");

        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from dbo.Studenti 
                where StudentiId  = " + id + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("U fshi me sukses");
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);

            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                select DepartmentName from dbo.Department
                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
