

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository,
    IBlobStorageService blobStorageService)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting restaurant with the id {request.Id}");
        var restaurnat = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurnat == null)
        {
            throw new NotFoundException($"Restaurant with {request.Id} does not exist");
        }
          
        var restaurnatDto = mapper.Map<RestaurantDto>(restaurnat);

        restaurnatDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurnat.LogoUrl);

        return restaurnatDto;
    }
}