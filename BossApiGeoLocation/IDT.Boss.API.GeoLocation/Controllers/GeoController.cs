
using System.Web.Http;

namespace IDT.Boss.API.GeoLocation.Controllers
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http.Description;

    using IDT.Boss.API.GeoLocation.Models;

    using NLog;

    using Oracle.ManagedDataAccess.Client;

    public class GeoController : ApiController
    {
        /// <summary>
        /// Gets Geo-location information based on provided IP
        /// </summary>
        /// <param name="ip">IP address</param>
        /// <returns>Geo location information</returns>
        [HttpGet]
        [Route("location-by-ip/{ip=''}")]
        [ResponseType(typeof(GeoLocationInfo))]
        public async Task<IHttpActionResult> LocationByIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return this.BadRequest("IP is empty");
            }
            using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["GEOIP"].ToString()))
            {
                IPAddress addr;
                if (!IPAddress.TryParse(ip, out addr))
                {
                    return this.BadRequest("Invalid IP address");
                }
                LogManager.GetLogger("Geo").Trace("Get ip for " + ip);
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GEOIP.GEOIP_PKG.GET_LOCATION";
                    cmd.BindByName = true;
                    var returnValParam = new OracleParameter(
                        "Return_Value",
                        OracleDbType.Int16,
                        ParameterDirection.ReturnValue);
                    var contryParam = new OracleParameter("O_COUNTRY", OracleDbType.Varchar2, ParameterDirection.Output)
                                          {
                                              Size = 255
                                          };
                    var stateParam = new OracleParameter(
                        "O_STATE",
                        OracleDbType.Varchar2,
                        ParameterDirection.Output)
                    {
                        Size = 255
                    };
                    var cityParam = new OracleParameter(
                        "O_CITY",
                        OracleDbType.Varchar2,
                        ParameterDirection.Output)
                    {
                        Size = 255
                    };
                    cmd.Parameters.Add(returnValParam);
                    cmd.Parameters.Add("IP_ADDR", OracleDbType.Varchar2, 255, ip, ParameterDirection.Input);
                    cmd.Parameters.Add(contryParam);
                    cmd.Parameters.Add(stateParam);
                    cmd.Parameters.Add(cityParam);
                    connection.Open();
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        var resultVal = int.Parse(returnValParam.Value.ToString());
                        if (resultVal == 1)
                        {


                            var locationinfo = new GeoLocationInfo
                                                   {
                                                       Ip = ip,
                                                       Country = contryParam.Value.ToString(),
                                                       City = cityParam.Value.ToString(),
                                                       State = stateParam.Value.ToString()
                                                   };

                            return this.Ok(locationinfo);
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = LogManager.GetLogger("GeoIP");
                        logger.Error(ex);
                        return this.InternalServerError(ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
