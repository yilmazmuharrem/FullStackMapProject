using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        Response response = new Response();
        private readonly DataContext _context;
        List<Location> _locations = new List<Location>();


        public LocationController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<Location> GetLocations()
        {
            var temp = from x in _context.Locations select x;

            foreach (var item in temp)
            {
                _locations.Add(item);
            }
            return _locations;
        }

        [HttpGet("id")]
        public Response GetLocationFromId(int id)
        {
            bool sonuc = false;
            Location locationx = new Location();
            var deleteOrderDetails =
           from x in _context.Locations
           where x.Id == id
           select x;

            foreach (var item in deleteOrderDetails)
            {
                sonuc = true;
                locationx = item;
            }
            if (sonuc)
            {
                response.Value = locationx;
                response.Result = "ARAMA İŞLEMİ BAŞARIYLA GERÇEKLEŞTİ";
                response.Status = true;
            }
            else
            {
                response.Result = " ARAMA İŞLEMİ BAŞARISIZ.";
                response.Status = false;
            }
            //    Location deneme = deleteOrderDetails.Single();
            return response;
        }

        [HttpPost]

        public Response Post(Location location)
        {
            bool sonuc = true;
            if (location.Name == "string" || location.X == 0 || location.Y == 0)
            {
                sonuc = false;
                response.Result = "GİRİLEN DEĞERLERDE HATA VAR !";
                response.Value = null;
                response.Status = false;
            }
            else
            {


                var tempLocation =
               from temp in _context.Locations
               where temp.Name == location.Name
               select temp;

                //    Location x= tempLocation.Single();
                foreach (Location item in tempLocation)
                {
                    if (string.Equals(item.Name, location.Name))
                    {
                        sonuc = false;
                        response.Result = " AYNI NAME DEĞERİNE AİT BİR DATA ZATEN MEVCUT";
                        response.Status = false;
                        //  response.Value = null;
                    }

                }
                if (sonuc)
                {
                    _context.Locations.Add(location);
                    _context.SaveChanges();
                    response.Result = "ASDASDAS";
                    response.Status = true;
                    response.Value = location;

                }


            }

            return response;
        }


        [HttpPut]
        public Response Put(Location location)
        {
            bool control = false;
            var temp = from x in _context.Locations where x.Id == location.Id select x;

            foreach (Location item in temp)
            {
                control = true;
                if (location.Name == "string" && location.X == 0 && location.Y == 0)
                {
                    control = false;
                    break;
                }
                if (control)
                {
                    if (location.Name == "string")
                    {
                        location.Name = item.Name;
                    }
                    if (location.X == 0)
                    {
                        location.X = item.X;
                    }
                    if (location.Y == 0)
                    {
                        location.Y = item.Y;
                    }
                    item.Name = location.Name;
                    item.X = location.X;
                    item.Y = location.Y;
                }
            }
            if (control)
            {
                _context.SaveChanges();
                response.Result = " İŞLEM BAŞARILI";
                response.Value = location;
                response.Status = true;

            }
            else
            {
                response.Result = " İŞLEM BAŞARISIZ";
                response.Status = false;

            }
            return response;



        }

        [HttpDelete]
        public Response delete(int id)
        {
            Location location = new Location();
            bool sonuc = false;
            var deleteOrderDetails =
           from x in _context.Locations
           where x.Id == id
           select x;

            foreach (var item in deleteOrderDetails)
            {
                location = item;
                sonuc = true;
                _context.Remove(item);

            }

            _context.SaveChanges();
            if (sonuc)
            {
                response.Value = location;
                response.Result = " SİLME İŞLEMİ BAŞARILI";
                response.Status = true;

            }
            else
            {
                response.Result = " SİLME İŞLEMİ BAŞARISIZ ";
                response.Status = false;
            }
            return response;

        }

    }
}
