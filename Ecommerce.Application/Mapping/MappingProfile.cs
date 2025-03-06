using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Dto.Box;
using Ecommerce.Application.Handlers.Barcodes.Commands;
using Ecommerce.Application.Handlers.Boxes.Commands;
using Ecommerce.Application.Handlers.Brand.Commands;
using Ecommerce.Application.Handlers.Categories.Commands;
using Ecommerce.Application.Handlers.City.Commands;
using Ecommerce.Application.Handlers.Colors.Commands;
using Ecommerce.Application.Handlers.Currencies.Commands;
using Ecommerce.Application.Handlers.Customers.Commands;
using Ecommerce.Application.Handlers.Stores.Commands;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Identity.Entities;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Category
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, CategoryDto>().ReverseMap();
        CreateMap<CategoryDto, Category>().ReverseMap();
        #endregion

        #region Product
        CreateMap<ProductForEditDto, Product>();
        CreateMap<ProductDto, Product>().ReverseMap();
        #endregion

        #region Color
        CreateMap<CreateColorCommand, Color>();
        CreateMap<UpdateColorCommand, Color>();
        CreateMap<UpdateColorCommand, ColorDto>().ReverseMap();
        CreateMap<ColorDto, Color>().ReverseMap();
        #endregion

        #region Size
        CreateMap<SizeDto, Size>().ReverseMap();
        #endregion

        #region OrderStatusValue
        CreateMap<OrderStatusValueDto, OrderStatusValue>().ReverseMap();
        #endregion

        #region DeliveryMethod
        CreateMap<DeliveryMethodDto, DeliveryMethod>().ReverseMap();
        #endregion

        #region Configuration
        CreateMap<GeneralConfigurationDto, GeneralConfiguration>().ReverseMap();

        CreateMap<SocialProfileDto, SocialProfile>().ReverseMap();

        CreateMap<HeaderSliderDto, HeaderSlider>().ReverseMap();
        CreateMap<DealOfTheDayDto, DealOfTheDay>().ReverseMap();
        CreateMap<BannerDto, Banner>().ReverseMap();

        CreateMap<FeatureProductConfigurationDto, FeatureProductConfiguration>().ReverseMap();

        CreateMap<StockConfigurationDto, StockConfiguration>().ReverseMap();

        CreateMap<TopCategoriesConfiguration, TopCategoriesConfigurationDto>().ReverseMap();

        CreateMap<BasicSeoConfiguration, BasicSeoConfigurationDto>().ReverseMap();

        CreateMap<SmtpConfiguration, SmtpConfigurationDto>().ReverseMap();


        CreateMap<SecurityConfiguration, SecurityConfigurationDto>().ReverseMap();


        CreateMap<AdvancedConfiguration, AdvancedConfigurationDto>().ReverseMap();
        #endregion

        #region City
        CreateMap<CreateCityCommand, City>();
        CreateMap<UpdateCityCommand, City>();
        CreateMap<UpdateCityCommand, CityDto>().ReverseMap();
        CreateMap<CityDto, City>().ReverseMap();
        #endregion

        #region Currency
        CreateMap<CreateCurrencyCommand, Currency>();
        CreateMap<UpdateCurrencyCommand, Currency>();
        CreateMap<UpdateCurrencyCommand, CurrencyDto>().ReverseMap();
        CreateMap<CurrencyDto, Currency>().ReverseMap();
        #endregion

        #region Store
        CreateMap<CreateStoreCommand,Store>();
        CreateMap<UpdateStoreCommand, Store>();
        CreateMap<UpdateStoreCommand, StoreDto>().ReverseMap();
        CreateMap<StoreDto, Store>().ReverseMap();
        #endregion

        #region Brand
        CreateMap<CreateBrandCommand, Brand>();
        CreateMap<UpdateBrandCommand, Brand>();
        CreateMap<UpdateBrandCommand, BrandDto>().ReverseMap();
        CreateMap<BrandDto, Brand>().ReverseMap();

        // Fix: Map Response<BrandDto> to UpdateBrandCommand
        CreateMap<Response<BrandDto>, UpdateBrandCommand>()
            .ConvertUsing(static src => src.Data != null ? new UpdateBrandCommand
            {
                Id = src.Data.Id,
                Name = src.Data.Name,
                Description = src.Data.Description,
                IsActive = src.Data.IsActive
            } : null);
        #endregion

        #region Box
        CreateMap<CreateBoxCommand, Boxes>();
        CreateMap<UpdateBoxCommand, Boxes>();
        CreateMap<UpdateBoxCommand, BoxesDto>().ReverseMap();
        CreateMap<BoxesDto, Boxes>().ReverseMap();
        CreateMap<Response<BoxesDto>, UpdateBoxCommand>()
            .ConvertUsing(static src => src.Data != null ? new UpdateBoxCommand
            {
                Id = src.Data.Id,
                Name = src.Data.Name,
                IsActive = src.Data.IsActive
            } : null);
        #endregion

        #region BarCode
        CreateMap<CreateBarcodeCommand, Barcode>();
        //CreateMap<UpdateStoreCommand, Store>();
        //CreateMap<UpdateStoreCommand, StoreDto>().ReverseMap();
        CreateMap<BarcodeDto, Barcode>().ReverseMap();
        #endregion

        #region BoxCurrencies
        CreateMap<BoxesCurrenciesDto, BoxesCurrencies>().ReverseMap();
        #endregion

        CreateMap<ContactQueryDto, ContactQuery>().ReverseMap();

        CreateMap<GalleryDto, Gallery>().ReverseMap();

        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
        CreateMap<OrderStatusDto, OrderStatus>().ReverseMap();

        CreateMap<ProductReviewDto, CustomerReview>().ReverseMap();

        CreateMap<ApplicationUser, EditProfileDto>().ReverseMap();

        CreateMap<IdentityRole, RoleDto>();
        CreateMap<IdentityRole, AddEditRoleDto>().ReverseMap();

        CreateMap<StockDto, Stock>().ReverseMap();

        CreateMap<ApplicationUser, UserDto>();
        CreateMap<ApplicationUser, AddEditUserDto>().ReverseMap();
        CreateMap<IdentityRole, ManageRoleDto>();

        CreateMap<CustomerDto, Customer>().ReverseMap();
        CreateMap<UpdateCustomerCommand, CustomerDto>().ReverseMap();
        CreateMap<UpdateCustomerCommand, Customer>();
        //CreateMap<User, CustomerRegisterDto>().ReverseMap();

        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer)).ReverseMap();


    }
}