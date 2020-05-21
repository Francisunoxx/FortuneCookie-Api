using System;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace Fortune
{
    [ApiController]
    [Route("[controller]")]
    public class FortuneController : ControllerBase
    {
        private string text = "";
        private Dictionary<string, string> json;
        public FortuneController()
        {
            //Read text from json
            text = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\\cookieSayings.json");
            //Deserialize string of cookies
            json = JsonSerializer.Deserialize<Dictionary<string, string>>(text);
        }

        [HttpGet("GetAllFortunes")]
        public IActionResult GetAllFortunes()
        {
            return Ok(json);
        }

        [HttpGet("GetFortune")]
        public IActionResult GetFortune()
        {
            Cookie cookie = new Cookie();
            string index = new Random().Next(1, 100).ToString();
            cookie.Key = json.Keys.ElementAt(Int32.Parse(index));
            cookie.Value = json.Values.ElementAt(Int32.Parse(index));

            return Ok(cookie);
        }

        [HttpPatch("UpdateFortune")]
        public IActionResult UpdateFortune(string key, string fortune)
        {
            json[key] = fortune;

            return Ok(json[key]);
        }
    }
}