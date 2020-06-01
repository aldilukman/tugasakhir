A. Instalasi
1. Install XAMPP untuk keperluan database
	https://www.apachefriends.org/index.html
2. Buat database dgn nama "tugasakhir" (Video ada di Folder "Video Configuration")
3. import file yg ada di folder database
4. Jalankan Program di folder "Program Jadi -> Debug -> TugasAkhir.exe ", pastikan sudah terinstall framework 4.6
	https://www.microsoft.com/en-us/download/details.aspx?id=48137
5. Untuk melihat source code bisa buka di Program Mentah ( saya memakai visual studio 2017 framework 4.6 )
6. Buka "Program mentah -> Login2-master -> Login2.sln" doubleclick

B. Setting Laptop/PC
1. Sambungkan laptop anda connect dengan ssid "Esp8266" dengan password "12345678"
2. Pastikan IP Laptop anda "192.168.4.2" biasanya otomatis jika tidak ada device lain yg connect dan pastikan juga hanya laptop anda yg connect ke esp agar ip nya selalu 192.168.4.2
3. Jika laptop IP anda masih tidak sesuai, anda bisa merubah secara manual ip nya di laptop anda, menjadi pengaturan ip static
	https://dosenit.com/jaringan-komputer/cara-merubah-ip-address-dynamic-menjadi-static
4. Jalankan XAMPP dan Pastikan APACHE dan MYSQL nya sudah start
![Screenshot (680)](https://user-images.githubusercontent.com/42825443/83310568-b06ba600-a236-11ea-9cff-da31e9eb7358.png)

C. Penggunaan program
1. Jalankan program TugasAkhir.exe ( Bagian A No 4)
2. Anda bisa mencari id/nama pelanggan

![Screenshot (684)](https://user-images.githubusercontent.com/42825443/83311405-4ef90680-a239-11ea-8167-d618012cfeb0.png)

3. Jika anda belum mempunyai id/nama pelanggan bisa insert ID Pelanggan

![Screenshot (685)](https://user-images.githubusercontent.com/42825443/83311413-57e9d800-a239-11ea-949c-e5235f1271bd.png)

4. Otomats data anda akan tersimpan di database
5. Lalu akan muncul page monotoring

![Screenshot (686)](https://user-images.githubusercontent.com/42825443/83311429-633d0380-a239-11ea-95e8-eedc73bf0103.png)

