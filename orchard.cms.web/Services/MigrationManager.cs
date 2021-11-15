using Microsoft.EntityFrameworkCore;
using infrastructure.cms.orchard.Models;
using infrastructure.sqlserver.Legacy;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore;
using OrchardCore.Autoroute.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentLocalization.Models;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.Media.Fields;
using OrchardCore.Seo.Models;
using OrchardCore.Title.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orchard.cms.web.Services
{
    public class MigrationManager : IMigrationManager
    {

        private readonly IContentManager _contentManager;
        private readonly IOrchardHelper _orchardHelper;
        private readonly IIdGenerator _iidGenerator;
        private readonly IServiceScopeFactory _scopeFactory;

        public MigrationManager(
            IContentManager contentManager,
            IOrchardHelper orchardHelper,
            IIdGenerator iidGenerator,
            IServiceScopeFactory scopeFactory
            )
        {
            _contentManager = contentManager;
            _orchardHelper = orchardHelper;
            _iidGenerator = iidGenerator;
            _scopeFactory = scopeFactory;
        }




        public async Task MigrateOrderableProductsAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LegacyDbContext>();

                var legacyParts = dbContext.Product_Infos
                    .AsQueryable()
                    .ToList();

                foreach (var legacyPart in legacyParts)
                    await MigrateOrderableProduct(legacyPart);


            }
        }


        private async Task MigrateOrderableProduct(Product_Info legacyPart)
        {


            var existingproduct = await _orchardHelper.GetContentItemBySlugAsync(legacyPart.Catalog_Number);
            if (existingproduct != null)
            {
                await _contentManager.RemoveAsync(existingproduct);
            }



            var newproduct = await _contentManager.NewAsync("OrderableProduct");
            newproduct.DisplayText = legacyPart.Catalog_Number;

            var newOrderableProduct = newproduct.As<OrderableProduct>();

            newOrderableProduct.Description = new TextField() { Text = legacyPart.Description };
            newOrderableProduct.PartNumber = new TextField() { Text = legacyPart.Catalog_Number };
            newOrderableProduct.ProductImage = new MediaField()
            {
                Paths = new string[] { $"product-images/{legacyPart.Catalog_Number}.png" },
                MediaTexts = new string[] { "" }
            };
            newproduct.Apply(newOrderableProduct);


            var newAutoroutePart = newproduct.As<AutoroutePart>();
            newAutoroutePart.Path = legacyPart.Catalog_Number.ToLower();
            newproduct.Apply(newAutoroutePart);


            var newTitlePart = newproduct.As<TitlePart>();
            newTitlePart.Title = legacyPart.Catalog_Number;
            newproduct.Apply(newTitlePart);


            var newLocalization = newproduct.As<LocalizationPart>();
            newLocalization.Culture = "en-US";
            newLocalization.LocalizationSet = _iidGenerator.GenerateUniqueId();
            newproduct.Apply(newLocalization);


            var newSeoMetaPart = newproduct.As<SeoMetaPart>();
            newSeoMetaPart.PageTitle = legacyPart.Catalog_Number;
            newSeoMetaPart.MetaKeywords = legacyPart.Catalog_Number;
            newSeoMetaPart.MetaDescription = $"Dataforth {legacyPart.Catalog_Number} {legacyPart.Description}";
            newSeoMetaPart.Canonical = $"https://www.dataforth.com/{legacyPart.Catalog_Number.ToLower()}";
            newproduct.Apply(newSeoMetaPart);


            await _contentManager.CreateAsync(newproduct, new VersionOptions());
            var valid2 = await _contentManager.ValidateAsync(newproduct);
            if (!valid2.Succeeded)
                throw new Exception("Exception trying to migrate " + legacyPart.Catalog_Number);

        }


    }
}
