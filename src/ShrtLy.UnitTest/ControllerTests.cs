using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shrtly.Common;
using ShrtLy.Api.Controllers;
using ShrtLy.Api.ResponceMapper;
using ShrtLy.Application.DTOs;
using ShrtLy.Application.Features;
using ShrtLy.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.UnitTest
{
    public class ControllerTests
    {
        public LinksController controller;
        public Mock<IResponseMapper> responceMock;
        public Mock<IMediator> mediatorMock;
        private const string _PREFIX = "https://localhost:5001/";

        public static List<LinkEntity> entities = new List<LinkEntity>
            {
                new LinkEntity
                {
                    Id = Guid.Parse("be27d1eb-a2a4-4590-a69c-cd32e0be6126"),
                    ShortUrl = "short-url-1",
                    Url = "url-1"
                },
                new LinkEntity
                {
                    Id = Guid.Parse("88829a87-e0db-408f-b8a9-9993862f01b7"),
                    ShortUrl = "short-url-2",
                    Url = "url-2"
                }
            };

        public static PaginatedList<LinkDto> linkDtos = new PaginatedList<LinkDto>(1, 1, 1)
            {
                new LinkDto
                {
                    Id = Guid.Parse("be27d1eb-a2a4-4590-a69c-cd32e0be6126"),
                    ShortUrl = "short-url-1",
                    Url = "url-1"
                },
                new LinkDto
                {
                    Id = Guid.Parse("88829a87-e0db-408f-b8a9-9993862f01b7"),
                    ShortUrl = "short-url-2",
                    Url = "url-2"
                }
            };

        [SetUp]
        public void Setup()
        {
            responceMock = new Mock<IResponseMapper>();
            mediatorMock = new Mock<IMediator>();
            controller = new LinksController(mediatorMock.Object, responceMock.Object);
        }

        [Test]
        public async Task GetShortLink_ProcessLinkHasBeenCalledAsync()
        {

            var str = _PREFIX + "an2jda";

            var mediatrResult = new Result<string> { ResultStatus = ResultStatus.Success, Data = str };

            var expectedResult = new ObjectResult(mediatrResult.Data)
            {
                StatusCode = StatusCodes.Status200OK
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetToShortLinkCommand>(), CancellationToken.None))
                .ReturnsAsync(mediatrResult);
            responceMock
           .Setup(m => m.ExecuteAndMapStatus(mediatrResult))
           .Returns(expectedResult);

            var result = await controller.GetToShortLink("http://google.com");

            result.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(expectedResult.StatusCode);
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedResult.Value);
        }

        [Test]
        public async Task GetShortLinks_GetLinksHasBeenCalledAsync()
        {
            var paginationDto = new PaginationDto() { Page = 1, PageSize = 1 };

            var mediatrResult = new Result<PaginatedList<LinkDto>> { ResultStatus = ResultStatus.Success, Data = linkDtos };

            var expectedResult = new ObjectResult(mediatrResult.Data)
            {
                StatusCode = StatusCodes.Status200OK
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllShortLinksQuery>(), CancellationToken.None)).ReturnsAsync(mediatrResult);
            responceMock
           .Setup(m => m.ExecuteAndMapStatus(mediatrResult))
           .Returns(expectedResult);

            var result  = await controller.GetShortLinks(paginationDto);

            result.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(expectedResult.StatusCode);
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedResult.Value);
        }

        [Test]
        public async Task GetShortLinks_AllLinksAreCorrectAsync()
        {
            var paginationDto = new PaginationDto() { Page = 1, PageSize = 1 };

            var mediatrResult = new Result<PaginatedList<LinkDto>> { ResultStatus = ResultStatus.Success, Data = linkDtos };

            var expectedResult = new ObjectResult(mediatrResult.Data)
            {
                StatusCode = StatusCodes.Status200OK
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllShortLinksQuery>(), CancellationToken.None)).ReturnsAsync(mediatrResult);
            responceMock
            .Setup(m => m.ExecuteAndMapStatus(mediatrResult))
            .Returns(expectedResult);

            var result = await controller.GetShortLinks(paginationDto);

            for (int i = 0; i < linkDtos.Count; i++)
            {
                Assert.AreEqual(entities[i].Id, linkDtos[i].Id);
                Assert.AreEqual(entities[i].ShortUrl, linkDtos[i].ShortUrl);
                Assert.AreEqual(entities[i].Url, linkDtos[i].Url);
            }
        }
    }
}
