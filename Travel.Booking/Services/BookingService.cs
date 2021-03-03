using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Travel.Booking.Dto;
using Travel.Booking.Models;

namespace Travel.Booking.Services
{
    public class BookingService
    {
        private readonly BookingRepository _repo;

        public BookingService(BookingRepository repo)
        {
            _repo = repo;
        }

        public bool IsExist(string code)
        {
            return _repo.Get(x => x.Code == code).Any();
        }

        public void Add(string code, string name)
        {
            _repo.Add(new User
            {
                Code = code,
                Name =  name,
                Id = Guid.NewGuid().ToString()
            });
        }

        public User GetByCode(string code)
        {
            var user = _repo.Get(x => x.Code == code).First();
            return user;
        }

        public User Get(string id)
        {
            return _repo.Get(id);
        }

        public void Update(UpdateDto dto)
        {
            var user = _repo.Get(dto.Id);
            user.Act1 = dto.Act1;
            user.Act2 = dto.Act2;
            user.Act3 = dto.Act3;
            user.ActOther = dto.ActOther;

            user.Eat1 = dto.Eat1;
            user.Eat2 = dto.Eat2;
            user.Eat3 = dto.Eat3;
            user.Eat4 = dto.Eat4;
            user.Eat5 = dto.Eat5;
            user.Eat6 = dto.Eat6;
            user.EatOther = dto.EatOther;

            user.Drink1 = dto.Drink1;
            user.Drink2 = dto.Drink2;
            user.Drink3 = dto.Drink3;
            user.Drink4 = dto.Drink4;

            user.DrinkOther = dto.DrinkOther;

            user.Alco1 = dto.Alco1;
            user.Alco2 = dto.Alco2;
            user.Alco3 = dto.Alco3;
            user.Alco4 = dto.Alco4;
            user.Alco5 = dto.Alco5;
            user.AlcoOther = dto.AlcoOther;

            _repo.Update(user);
        }

        public List<User> GetAll()
        {
            return _repo.Get(x => true).ToList();
        }
    }
}