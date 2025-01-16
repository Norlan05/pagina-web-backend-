using CLINICA.Data;
using CLINICA.Model_request;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace CLINICA.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        ClinicaDbcontext db;
        public UpdateController(ClinicaDbcontext db) 
        {
            this.db = db;
        }
        [HttpPost]
        [Route("password")]
        public IActionResult Update_password(ResetDto model)
        {
            GeneradorNumerosAleatorios generate = new();
            var user = db.usuarios.Where(c => c.email == model.email).Select(a => a).FirstOrDefault();

            if (user == null)
            {
                errorModel error = new errorModel
                {
                    error_text = "valide las credenciales nuevamente",
                    status_error = "Error en reset"
                };
                return BadRequest(error);
            }

            user.ResetToken = generate.generar_pass();
            user.ResetTokenExpiry = DateTime.Now;

            //reseteamos
            db.usuarios.Update(user);
            db.SaveChanges();

            

            string htmlTemplate = @"
            <!DOCTYPE html>
            <html lang='es'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Correo Estético</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        color: #333;
                        background-color: #f4f4f4;
                        margin: 0;
                        padding: 0;
                    }
                    .email-container {
                        width: 600px;
                        margin: 20px auto;
                        padding: 20px;
                        background-color: #ffffff;
                        border-radius: 10px;
                        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
                    }
                    h1 {
                        color: #4CAF50;
                    }
                    p {
                        font-size: 16px;
                        color: #666;
                    }
                    .footer {
                        text-align: center;
                        font-size: 12px;
                        color: #999;
                        margin-top: 20px;
                    }
                </style>
            </head>
            <body>
                <div class='email-container'>
                    <h1>¡Hola!</h1>
                    <p>Gracias por usar nuestro servicio. Aquí está el mensaje que te enviamos:</p>
                    <p>su contraseña temporal es: {0}</p>
                    <div class='footer'>
                        <p>Este es un correo automático, por favor no respondas a este correo.</p>
                    </div>
                </div>
            </body>
            </html>";

            string emailContent = htmlTemplate.Replace("{0}", user.ResetToken);
            Enviaremail(model.email,"Reset de contraseña", emailContent);

            return Ok();

        }
        public class GeneradorNumerosAleatorios
        {
            public string generar_pass()
            {
                // Crear una instancia del generador de números aleatorios
                Random random = new Random();
                string password = "";
                // Generar y mostrar 5 números aleatorios entre 0 y 9
                for (int i = 0; i < 5; i++)
                {
                    int numero = random.Next(0, 10); // Genera un número entre 0 y 9
                    password += numero.ToString();
                }
                return password;
            }
        }

        public static bool Enviaremail(string correo, string asunto, string mensaje)
        {

            bool result = false;
            try
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(correo);
                mail.From = new MailAddress("citasclinicadrtoruno@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smpt = new SmtpClient()
                {
                    Credentials = new NetworkCredential("citasclinicadrtoruno@gmail.com", "ysdawylalosstpmt"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smpt.Send(mail);
                result = true;
            }
            catch (Exception e)
            {

                throw;
            }


            return result;
        }
    }
            
 }