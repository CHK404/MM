using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows.Input;
using Material_Management.Models;
using Material_Management.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Threading.Channels;
using System.Windows.Media;

namespace Material_Management.ViewModels
{
    public partial class MaterialViewModel : ObservableObject
    {
        public ObservableCollection<Material> Materials { get; } = new ObservableCollection<Material>();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveMaterialCommand))]
        [NotifyCanExecuteChangedFor(nameof(EditMaterialCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteMaterialCommand))]
        private Material? selectedMaterial;
        partial void OnSelectedMaterialChanged(Material? value)
        {
            EditMaterialCommand.NotifyCanExecuteChanged();
            DeleteMaterialCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private bool isPopupOpen;

        public IRelayCommand ViewDetailsCommand { get; }
        public IRelayCommand EditMaterialCommand { get; }
        public IRelayCommand DeleteMaterialCommand { get; }
        public IRelayCommand AddMaterialCommand { get; }
        public IRelayCommand SaveMaterialCommand { get; }
        public IRelayCommand CancelCommand { get; }

        private bool CanModifyMaterial(Material? material) => material != null;
        public MaterialViewModel()
        {
            ViewDetailsCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<Material>(ViewDetails);
            EditMaterialCommand = new RelayCommand(EditMaterial, () => SelectedMaterial != null);
            DeleteMaterialCommand = new RelayCommand(DeleteMaterial, () => SelectedMaterial != null);
            AddMaterialCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(OpenAddMaterialPopup);
            SaveMaterialCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(async () => await SaveMaterialAsync(), CanSaveMaterial);
            CancelCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(Cancel);

            _ = LoadMaterialsAsync();
        }

        public async Task LoadMaterialsAsync()
        {
            try
            {
                using var db = new AppDbContext();
                var materials = await db.Materials.ToListAsync();
                Materials.Clear();
                foreach (var material in materials)
                {
                    Materials.Add(material);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewDetails(Material? material)
        {
            if (material == null)
                return;

            string details = $"일련번호: {material.SerialNumber}\n" +
                             $"자재 이름: {material.MaterialName}\n" +
                             $"자재 종류: {material.MaterialType}\n" +
                             $"제조사: {material.Manufacturer}\n" +
                             $"수량: {material.Quantity}\n" +
                             $"사용자 ID: {material.UserID}\n" +
                             $"정보 URL: {material.InfoURL}\n" +
                             $"상세정보: {material.Content}";
            MessageBox.Show(details, "자재 상세정보", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void EditMaterial()
        {
            if (SelectedMaterial == null)
                return;
            IsPopupOpen = true;
        }
        private void OpenAddMaterialPopup()
        {
            SelectedMaterial = new Material
            {
                SerialNumber = Guid.NewGuid().ToString(),
                MaterialName = "",
                MaterialType = "",
                Manufacturer = "",
                InfoURL = "",
                Content = "",
                Quantity = 1,
                UserID = CurrentUser.UserID
            };
            IsPopupOpen = true;
        }

        private async Task SaveMaterialAsync()
        {
            if (SelectedMaterial == null)
                return;

            try
            {
                using var db = new AppDbContext();

                bool exists = await db.Materials.AnyAsync(m => m.SerialNumber == SelectedMaterial.SerialNumber);

                if (!exists)
                {
                    db.Materials.Add(SelectedMaterial);
                    await db.SaveChangesAsync();

                    Materials.Add(SelectedMaterial);
                }
                else
                {
                    db.Attach(SelectedMaterial);
                    db.Entry(SelectedMaterial).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                IsPopupOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanSaveMaterial() => SelectedMaterial != null;
        private async void DeleteMaterial()
        {
            if (SelectedMaterial == null)
                return;

            if (MessageBox.Show("정말 삭제하시겠습니까?", "삭제 확인",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            using var db = new AppDbContext();
            db.Materials.Remove(SelectedMaterial);
            await db.SaveChangesAsync();
            Materials.Remove(SelectedMaterial);
            SelectedMaterial = null;
        }
        private void Cancel()
        {
            SelectedMaterial = null;
            IsPopupOpen = false;
        }
    }
}
