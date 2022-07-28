using Acr.UserDialogs;
using Android.Graphics;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LB_Chopp.ViewModels
{
    public class ImagemPageViewModel : ViewModelBase
    {
        ReservaChopp Reserva { get; set; }
        string Origem { get; set; }
        string fotoBase64 { get; set; }

        private string _fotopath = string.Empty;
        public string FotoPath { get { return _fotopath; } set { SetProperty(ref _fotopath, value); } }

        public DelegateCommand SalvarCommand { get; }
        public DelegateCommand TirarFotoCommand { get; }
        public DelegateCommand BuscarFotoCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ImagemPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            SalvarCommand = new DelegateCommand(async () =>
            {
                if(string.IsNullOrWhiteSpace(fotoBase64))
                {
                    await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório selecionar foto.", "OK");
                    return;
                }
                var ret = await dialogService.DisplayAlertAsync("Pergunta", "Salvar Imagem?", "SIM", "NÃO");
                if(ret)
                    using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                    {
                        try
                        { 
                            ReservaFoto foto = new ReservaFoto();
                            foto.Cd_empresa = Reserva.Cd_empresa;
                            foto.Id_reserva = Reserva.Id_reserva;
                            foto.Foto = fotoBase64;
                            foto.Origem = Origem;
                            string retorno = await dataService.GravarFotoAsync(foto);
                            if (retorno.Trim().Equals("1"))
                            {
                                await dialogService.DisplayAlertAsync("Mensagem", "Foto gravada com sucesso.", "OK");
                                await navigationService.GoBackAsync();
                            }
                            else await dialogService.DisplayAlertAsync("Erro", retorno, "OK");
                        }
                        catch(Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                    }
            });

            TirarFotoCommand = new DelegateCommand(async () =>
            {
                try
                {
                    var foto = await MediaPicker.CapturePhotoAsync();
                    string path = System.IO.Path.Combine(FileSystem.AppDataDirectory, foto.FileName);
                    using (var stream = await foto.OpenReadAsync())
                    {
                        using (var newStream = File.OpenWrite(path))
                            await stream.CopyToAsync(newStream);
                    }
                    byte[] buffer = File.ReadAllBytes(path);
                    Bitmap original = BitmapFactory.DecodeByteArray(buffer, 0, buffer.Length);
                    Bitmap comp = Bitmap.CreateScaledBitmap(original, 1080, 1080, false);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        comp.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                        fotoBase64 = Convert.ToBase64String(ms.ToArray());
                    }
                    FotoPath = path;
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", fnsEx.Message.Trim(), "OK");
                }
                catch (PermissionException pEx)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", pEx.Message.Trim(), "OK");
                }
                catch (Exception ex)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", $"Erro: {ex.Message}", "OK");
                }
            });

            BuscarFotoCommand = new DelegateCommand(async () =>
            {
                try
                {
                    var foto = await MediaPicker.PickPhotoAsync();
                    string path = System.IO.Path.Combine(FileSystem.AppDataDirectory, foto.FileName);
                    using (var stream = await foto.OpenReadAsync())
                    {
                        using (var newStream = File.OpenWrite(path))
                            await stream.CopyToAsync(newStream);
                    }
                    fotoBase64 = Convert.ToBase64String(File.ReadAllBytes(path));
                    FotoPath = path;
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", fnsEx.Message.Trim(), "OK");
                }
                catch (PermissionException pEx)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", pEx.Message.Trim(), "OK");
                }
                catch (Exception ex)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", $"Erro: {ex.Message}", "OK");
                }
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("RESERVA"))
                Reserva = parameters["RESERVA"] as ReservaChopp;
            if (parameters.ContainsKey("ORIGEM"))
                Origem = parameters["ORIGEM"].ToString();
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                FotoPath = null;
                return;
            }
            // save the file into local storage
            using (var stream = await photo.OpenReadAsync())
            {
                using (var newStream = File.OpenWrite(FotoPath))
                    await stream.CopyToAsync(newStream);
            }
        }
    }
}
