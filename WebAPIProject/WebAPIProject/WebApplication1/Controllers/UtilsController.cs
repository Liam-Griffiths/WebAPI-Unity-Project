using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class UtilsController : ApiController
    {

        [Route("api/Status")]
        public IHttpActionResult IsServerAlive()
        {
            return Ok("alive");
        }

        [Route("api/Test")]
        public IHttpActionResult Test()
        {
            //string timenow = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
            //return Ok(LoginUtils.encryptToken("% u,{G  l\u0003I0    X 7:\u001f ~\u001e     _B\u001eq'\u0006㑋  \f    P ҁګ  F \u0011\t   ݾ D  du \u0003  \u00051_  Z\u0002 \u0018۩  .No\u0003k \u0002? r\t  ]Q  o .\u001f +  I67 \t,  ʂ  \\ \u0013Zۉt ~kI  p BR  A  \u001ea\r x E ٓ0 h   6 {0Tt\f0f,\u000b 6 Fs | ^ ZE   lJS@W  d dO\u0007   qv   p    [  \u001f \u001d  L V Ձ 5+UN   Tgʩq cc Vc \u0003 ", timenow));
            decryptTokenData data = LoginUtils.decryptToken("JSB1LHtHICBsA0kwICAgIFggNzofIH4eICAgICBfQh5xJwbjkYsgIAwgICB/IFAg0oHaqyAgRiARCSAgIN2+IEQgIGR1IAMgIAUxXyAgWgIgGNupICAuTm8DayACPyByCSAgXVEgIG8gLh8gKyAgSTY3IAksICDKgiAgXCATWtuJdCB+a0kgIHAgQlIgIEEgIB5hDSB4IEUg2ZMwIGggICA2IHswVHQMMGYsCyA2IEZzIHwgXiBaRSAgIGxKU0BXICBkIGRPByAgIHF2ICAgcCAgICBbICAfIB0gIEwgViDVgSA1K1VOICAgVGfKqXEgY2MgVmMgAyAyNS8wNi8yMDE1IDE1OjIzOjAz");
            return Ok(data);
        }

    }
}
