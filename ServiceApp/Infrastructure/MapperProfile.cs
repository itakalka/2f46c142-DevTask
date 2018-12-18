using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using AutoMapper;

namespace ServiceApp.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile(JavaScriptSerializer serializer)
        {
            if (serializer is null) throw new ArgumentNullException(nameof(serializer));

            CreateMap<Dictionary<string, string>, ExtraData.ExtraData>()
                .ConvertUsing(x => serializer.Deserialize<ExtraData.ExtraData>(serializer.Serialize(x)));
        }
    }
}