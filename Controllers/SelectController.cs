using CLINICA.Model_request;
using Microsoft.AspNetCore.Mvc;
using CLINICA.Modelos;
using CLINICA.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CLINICA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SelectController : ControllerBase
    {
        ClinicaDbcontext db;

        public SelectController(ClinicaDbcontext db)
        {
            this.db = db;
        }


        [HttpPost]
        public IActionResult Index(Cliente_search model)
        {


            //coneccion base de datos

            // var cliente_hoy = db.clientes.where(c => c.email == model.email).select(c => c).firstOrderDefaul();

            //


            //db.add(cliente)
            //db.savechange()


            //

            clientes cliente_encontrado = new clientes
            {
                //      id_cliente = cliente_hoy.id_cliente,
                nombre = "C la come",
                apellido = "doblada",
            };
            return Ok(cliente_encontrado);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> get_user(UserDTO model)
        {
            (var error_in_requet, bool validate) = validate_request(model);
            if (validate) return BadRequest(error_in_requet);

            var user = db.usuarios.Where(c => c.email == model.email && c.password == model.password && c.is_active == true).Select(a => a).FirstOrDefault();

            if (user == null)
            {
                errorModel error = new errorModel
                {
                    error_text = "valide las credenciales nuevamente",
                    status_error = "Error en login"
                };
                return BadRequest(error);
            }

            if (user.reset)
            {
                user.reset = false;
                var update_user = db.usuarios.Update(user);
                db.SaveChanges();
                return Ok(true);
            }
            else return (Ok(true));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public (errorModel, bool) validate_request(UserDTO model) 
        {
            errorModel error = new errorModel();

            if (string.IsNullOrEmpty(model.email))
            {
                error.status_error = "campo email es necesario";
                error.error_text = "campo requerido";
                return (error, true);
            }
            else if (string.IsNullOrEmpty(model.password)) 
            {
                error.status_error = "campo password es necesario";
                error.error_text = "campo requerido";
                return (error, true);
            }
            return (error, false);
        }
    }
}
