using CobaNetCore.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace CobaNetCore.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IConfiguration _configuration;


        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getEmployee")]
        public IActionResult getEmployee()
        {
            db_functions db = new db_functions(_configuration);
            //var options = new jsonserializeroptions
            try
            {
                string hasil = db.GetEmployee();
                if(hasil != "")
                {
                    clsEmployeeResp resp = new clsEmployeeResp();
                    resp.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    resp.error = "0";
                    string[] arr = Regex.Split(hasil, "\r\n?|\n");
                    resp.employees = new clsEmployee[arr.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string value = arr[i];
                        if (value.Length > 0)
                        {
                            resp.employees[i] = new clsEmployee();
                            string[] temp = arr[i].Split('|');
                            resp.employees[i].id = temp[0];
                            resp.employees[i].name = temp[1];
                            resp.employees[i].address = temp[2];
                            resp.employees[i].salary = temp[3];
                        }
                    }
                    OkObjectResult result = new OkObjectResult(resp);
                    return result;
                }
                else
                {
                    clsRespError respError = new clsRespError();
                    respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    respError.error = "1";
                    respError.message = "Belum ada karyawan";
                    OkObjectResult result = new OkObjectResult(respError);
                    return result;
                }
            }
            catch(Exception ex)
            {
                clsRespError respError = new clsRespError();
                respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                respError.error = "1";
                respError.message = ex.Message;
                OkObjectResult result = new OkObjectResult(respError);
                return result;
            }
        }

        [HttpPost]
        [Route("insertEmployee")]
        public IActionResult insertEmployee(string name, string address, int salary)
        {
            db_functions db = new db_functions(_configuration);
            //var options = new jsonserializeroptions
            try
            {
                if (db.InsertEmployee(name, address, salary))
                {
                    clsRespError resp = new clsRespError();
                    resp.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    resp.error = "0";
                    resp.message = "Berhasil insert employee";
                    OkObjectResult result = new OkObjectResult(resp);
                    return result;
                }
                else
                {
                    clsRespError respError = new clsRespError();
                    respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    respError.error = "1";
                    respError.message = "Gagal insert karyawan";
                    OkObjectResult result = new OkObjectResult(respError);
                    return result;
                }
            }
            catch (Exception ex)
            {
                clsRespError respError = new clsRespError();
                respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                respError.error = "1";
                respError.message = ex.Message;
                OkObjectResult result = new OkObjectResult(respError);
                return result;
            }
        }

        [HttpPatch]
        [Route("updateEmployee")]
        public IActionResult updateEmployee(string id, string name, string address, int salary)
        {
            db_functions db = new db_functions(_configuration);
            //var options = new jsonserializeroptions
            try
            {
                if (db.UpdateEmployee(id, name, address, salary))
                {
                    clsRespError resp = new clsRespError();
                    resp.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    resp.error = "0";
                    resp.message = "Berhasil update employee";
                    OkObjectResult result = new OkObjectResult(resp);
                    return result;
                }
                else
                {
                    clsRespError respError = new clsRespError();
                    respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    respError.error = "1";
                    respError.message = "Gagal update karyawan";
                    OkObjectResult result = new OkObjectResult(respError);
                    return result;
                }
            }
            catch (Exception ex)
            {
                clsRespError respError = new clsRespError();
                respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                respError.error = "1";
                respError.message = ex.Message;
                OkObjectResult result = new OkObjectResult(respError);
                return result;
            }
        }

        [HttpDelete]
        [Route("deleteEmployee")]
        public IActionResult deleteEmployee(string id)
        {
            db_functions db = new db_functions(_configuration);
            //var options = new jsonserializeroptions
            try
            {
                if (db.DeleteEmployee(id))
                {
                    clsRespError resp = new clsRespError();
                    resp.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    resp.error = "0";
                    resp.message = "Berhasil delete employee";
                    OkObjectResult result = new OkObjectResult(resp);
                    return result;
                }
                else
                {
                    clsRespError respError = new clsRespError();
                    respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                    respError.error = "1";
                    respError.message = "Gagal delete karyawan";
                    OkObjectResult result = new OkObjectResult(respError);
                    return result;
                }
            }
            catch (Exception ex)
            {
                clsRespError respError = new clsRespError();
                respError.statusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                respError.error = "1";
                respError.message = ex.Message;
                OkObjectResult result = new OkObjectResult(respError);
                return result;
            }
        }
    }
}
