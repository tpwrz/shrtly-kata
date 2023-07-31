﻿using Microsoft.AspNetCore.WebUtilities;
using Shrtly.Common;
using ShrtLy.Application.DTOs;
using ShrtLy.Contracts;
using ShrtLy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.Application.Services
{
    public class ShorteningService : IShorteningService
    {
        private readonly ILinksRepository repository;

        public ShorteningService(ILinksRepository repository)
        {
            this.repository = repository;
        }

        //public async Task<LinkEntity> ProcessLink(string url)
        //{
        //    var entity = this.repository.GetAllLinks().Where(x => x.Url == url).FirstOrDefault();
        //    if (entity == null)
        //    {
        //        Thread.Sleep(1);//make everything unique while looping
        //        long ticks = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalMilliseconds;//EPOCH
        //        char[] baseChars = new char[] { '0','1','2','3','4','5','6','7','8','9',
        //    'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        //    'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'};

        //        int i = 32;
        //        char[] buffer = new char[i];
        //        int targetBase = baseChars.Length;

        //        do
        //        {
        //            buffer[--i] = baseChars[ticks % targetBase];
        //            ticks = ticks / targetBase;
        //        }
        //        while (ticks > 0);

        //        char[] result = new char[32 - i];
        //        Array.Copy(buffer, i, result, 0, 32 - i);

        //        var shortUrl = new string(result);

        //        var link = new LinkEntity
        //        {
        //            ShortUrl = shortUrl,
        //            Url = url
        //        };

        //        return link;
        //    }
        //    else
        //    {
        //        return entity;
        //    }
        //}


        public async Task<string> ProcessLink(string url)
        {// Test our URL
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
            {
                var StatusCode = ResultStatus.BadRequest;
                return null;
            }
            
            var link = new LinkDto
            {
                Id = Guid.NewGuid(),
                ShortUrl = result.ToString(),
                Url = url
            };
            var urlChunk = link.GetUrlChunk();

            return urlChunk;
        }
    }
}
