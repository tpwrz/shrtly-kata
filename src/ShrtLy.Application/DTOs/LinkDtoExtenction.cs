using Microsoft.AspNetCore.WebUtilities;
using System;

namespace ShrtLy.Application.DTOs
{
    public static class LinkDtoExtenction
    {
        public static string GetUrlChunk(this LinkDto linkDto)
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(GetId(linkDto.Id.ToString())));
        }

        public static int GetId(string urlChunk)
        {
            // Reverse our short url text back into an interger Id
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(urlChunk));
        }
    }
}

