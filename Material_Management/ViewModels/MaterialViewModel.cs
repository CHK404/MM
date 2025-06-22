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

namespace Material_Management.ViewModels
{
    public class MaterialViewModel : ObservableObject
    {
        public ObservableCollection<Material> Materials { get; } = new ObservableCollection<Material>();

        public ICommand ViewDetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddMaterialCommand { get; }

        public MaterialViewModel()
        {
            ViewDetailsCommand = new RelayCommand<Material>(ViewDetails);
            EditCommand = new RelayCommand<Material>(EditMaterial);
            DeleteCommand = new RelayCommand<Material>(DeleteMaterial);
            AddMaterialCommand = new RelayCommand(AddMaterial);
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
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void EditMaterial(Material? material)
        {
            if (material == null)
                return;
            try
            {
                using var db = new AppDbContext();
                db.Materials.Update(material);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"수정 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteMaterial(Material? material)
        {
            if (material == null)
                return;

            if (MessageBox.Show("정말 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            try
            {
                using var db = new AppDbContext();
                db.Materials.Remove(material);
                await db.SaveChangesAsync();
                Materials.Remove(material);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddMaterial()
        {
            var newMaterial = new Material
            {
                SerialNumber = "",
                MaterialName = "자재명",
                MaterialType = "재료",
                Manufacturer = "제조사",
                Quantity = 1,
                UserID = CurrentUser.UserID
            };

            try
            {
                using var db = new AppDbContext();
                db.Materials.Add(newMaterial);
                await db.SaveChangesAsync();
                Materials.Add(newMaterial);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"추가 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
