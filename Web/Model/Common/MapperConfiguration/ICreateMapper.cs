﻿using AutoMapper;

namespace Web.Model.Common.MapperConfiguration
{
   public interface ICreateMapper<TSource>
    {
        void Map(Profile profile)
        {
            profile.CreateMap(typeof(TSource), GetType()).ReverseMap();
        }
    }
}
