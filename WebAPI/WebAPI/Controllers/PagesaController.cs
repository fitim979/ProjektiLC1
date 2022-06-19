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
    public class PagesaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PagesaController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select NrId, EmriMbiemri, Niveli, Drejtimi, Faturimi, Pagesaa, 
                 Mbetja
                from dbo.Pagesa
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
        public JsonResult Post(Pagesa pa)
        {
            string query = @"
                        insert into dbo.Pagesa 
                    (NrId,EmriMbiemri,Niveli,Drejtimi,Faturimi,Pagesaa,Mbetja)
                    values
                    (
                    '" + pa.NrId + @"'
                    ,'" + pa.EmriMbiemri + @"'
                    ,'" + pa.Niveli + @"'
                    ,'" + pa.Drejtimi + @"'
                    ,'" + pa.Faturimi + @"'
                    ,'" + pa.Pagesaa + @"'
                    ,'" + pa.Mbetja + @"'
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
        public JsonResult Put(Pagesa pa)
        {
            string query = @"
                        update dbo.pagesa set 
                NrId ='" + pa.NrId + @"'
                ,EmriMbiemri ='" + pa.EmriMbiemri + @"'
                ,Niveli ='" + pa.Niveli + @"'
                ,Drejtimi ='" + pa.Drejtimi + @"'
                ,Faturimi ='" + pa.Faturimi + @"'
                ,Pagesaa ='" + pa.Pagesaa + @"'
                ,Mbetja ='" + pa.Mbetja + @"'
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
                        delete from dbo.Pagesa 
                where NrId = " + id + @"
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



    }
}
