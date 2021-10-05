using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Text;
using lab.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using lab.Data;

namespace lab.Data
{
  public class SampleData
  {
    public static void Initialize(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        // Look for any Provinces.
        if (context.Provinces.Any())
        {
          return;   // DB has already been seeded
        }

        var provinces = GetProvinces().ToArray();
        context.Provinces.AddRange(provinces);
        context.SaveChanges();

        var cities = GetCities(context).ToArray();
        context.Cities.AddRange(cities);
        context.SaveChanges();
      }
    }

    public static List<Province> GetProvinces()
    {
      List<Province> provinces = new List<Province>() {
              new Province() {
                  ProvinceCode="BC",
                  ProvinceName="British Coulumbia",
              },
              new Province() {
                ProvinceCode="ON",
                ProvinceName="Ontatrio",
              },
              new Province() {
                ProvinceCode="IC",
                ProvinceName="Incheon",
              },
              new Province() {
                ProvinceCode="SL",
                ProvinceName="Seoul",
              }
          };

      return provinces;
    }

    public static List<City> GetCities(ApplicationDbContext context)
    {
      List<City> Cities = new List<City>() {
              new City {
                  CityName = "Vancouver",
                  Population = 10000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode
              },
              new City {
                  CityName = "Kelowna",
                  Population = 5000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode
              },
              new City {
                  CityName = "Kamloops",
                  Population = 3000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode
              },
              new City {
                  CityName = "Incheon",
                  Population = 100000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode

              },
          };

      return Cities;
    }
  }

}
