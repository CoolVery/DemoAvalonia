using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using Demo.Models;
using Demo.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using SkiaSharp;

namespace Demo.ViewModels
{
    public class ListPartnersViewModel : ReactiveObject
    {
        int sortRezim;
        List<Partner> partners;
        List<Partner> partnersCopy;
        List<Partner> selectedPartners;
        string namePartner = "";
        List<PartnerType> partnersType;
        PartnerType selectedPartnerType;
        List<string> sortedValues = new List<string>() { "Все типы", "Наименование", "Адрес" };
        string selectedSortedValue = "Все типы";
        string currentCount;
        string allCount;
        public List<string> SortedValues { get => sortedValues; set => this.RaiseAndSetIfChanged(ref sortedValues, value); }
        public string SelectedSortedValue
        {
            get => selectedSortedValue;
            set
            {
                selectedSortedValue = value;
                AllFilters();
                
            }
        }
        public List<Partner> Partners { get => partners; set => this.RaiseAndSetIfChanged(ref partners, value); }
        public string NamePartner
        {
            get => namePartner;
            set
            {
                this.RaiseAndSetIfChanged(ref namePartner, value);
                AllFilters();
            }
        }

        public List<PartnerType> PartnersType { get => partnersType; set => this.RaiseAndSetIfChanged(ref partnersType, value); }
        public PartnerType SelectedPartnerType
        {
            get => selectedPartnerType;
            set
            {
                selectedPartnerType = value;
                AllFilters();
            }
        }

        public string CurrentCount { get => currentCount; set => this.RaiseAndSetIfChanged(ref currentCount, value); }
        public string AllCount { get => allCount; set => this.RaiseAndSetIfChanged(ref allCount, value); }
        public List<Partner> SelectedPartners { get => selectedPartners; 
            set { 
                this.RaiseAndSetIfChanged( ref selectedPartners , value);
            } 
        }
        int ind =0;
        public void Update() {
            SelectedPartners.ForEach(x =>ind++);
        }

        //public List<Partner> SelectedPartners { 
        //    get 
        //    {
        //        for (int i = 0; i < 15; i++)
        //        {

        //        }
        //        return selectedPartners;
        //    }
        //     set { selectedPartners = value;
        //        for (int i = 0; i < 15; i++)
        //        {

        //        }
        //    } 
        //public async Task Image()
        //{
        //    if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
        //        desktop.MainWindow?.StorageProvider is not { } provider)
        //        throw new NullReferenceException("Missing StorageProvider instance.");
        //    var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        //    {
        //        Title = "Выберите фото для аватарки",
        //        AllowMultiple = false
        //    });
        //    await using var readStream = await files[0].OpenReadAsync();
        //    byte[] buffer = new byte[1000000];
        //    var bytes = readStream.ReadAtLeast(buffer, 1);
        //    Array.Resize(ref buffer, bytes);
        //    Current_user.Image = buffer;
        //    ImageUser = new Bitmap(new MemoryStream(current_user.Image));
        //    myContext.SaveChanges();
        //    MainWindowViewModel.Self.UserPageVM = new UserPageViewModel(Current_user.IdUser);
        //    MainWindowViewModel.Self.Page = new UserPage();
        //}
        //ConvertImage = user.Image != null ? new Bitmap(new MemoryStream(user.Image)) : new Bitmap("Assets\\ava.png")
        //}

        public ListPartnersViewModel()
        {

            partners = MainWindowViewModel.Inctance.Context.Partners.Include(x => x.PartnerType).ToList();
            partnersCopy = new List<Partner>(partners);
            partnersType = MainWindowViewModel.Inctance.Context.PartnerTypes.ToList();
            partnersType.Add(new PartnerType() { PartnerTypeName = "Нет фильтра" });
            allCount = partners.Count.ToString();
            currentCount = allCount;
        }
        void AllFilters()
        {

            SearchPartnerName();
            SerchPartnerType();
            SortedListForValue();
            CurrentCount = Partners.Count.ToString();
        }
        void SearchPartnerName()
        {
            switch (string.IsNullOrEmpty(namePartner))
            {
                case true:
                    Partners = partnersCopy;
                    break;
                case false:
                    Partners = partnersCopy.Where(partner => partner.PartnerName.Contains(namePartner)).ToList();
                    break;
            }
        }
        void SerchPartnerType()
        {
            switch (selectedPartnerType == null || selectedPartnerType.PartnerTypeName == "Нет фильтра")
            {
                case true:
                    break;
                case false:
                    Partners = Partners.Where(partners => partners.PartnerType == SelectedPartnerType).ToList();
                    break;
            }
        }
        public void ToAddNewPartner()
        {
            MainWindowViewModel.Inctance.CurrentUC = new NewPartner();
        }
        void SortedListForValue()
        {
            switch (selectedSortedValue)
            {
                case "Наименование":
                    switch (sortRezim)
                    {
                        case 1:
                            Partners = Partners.OrderBy(partner => partner.PartnerName).ToList();
                            break;
                        case 2:
                            Partners = Partners.OrderByDescending(partner => partner.PartnerName).ToList();
                            break;
                        default:
                            Partners = Partners.OrderBy(partner => partner.PartnerName).ToList();
                            break;
                    }
                    break;
                case "Адрес":

                    switch (sortRezim)
                    {
                        case 1:
                            Partners = Partners.OrderBy(partner => partner.PartnerDirectorCredentials).ToList();
                            break;
                        case 2:
                            Partners = Partners.OrderByDescending(partner => partner.PartnerDirectorCredentials).ToList();
                            break;
                        default:
                            Partners = Partners.OrderBy(partner => partner.PartnerDirectorCredentials).ToList();
                            break;
                    }
                    break;

            }
        }
        public void SortMode(int id)
        {
            sortRezim = id;
            AllFilters();
        }
    }
}