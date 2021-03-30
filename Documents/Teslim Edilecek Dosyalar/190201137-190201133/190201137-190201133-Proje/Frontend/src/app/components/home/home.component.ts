import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }
  array = [
    {
      title:"Frekans Hesaplama", 
      desc: "Website içerisinde kelimelerin kaç defa tekrar ettiğini hesapla",
      img: "assets/image/home_1.jpg"
    },
    {
      title:"Anahtar Kelime Hesaplama", 
      desc: "Website içerisinde önemli olan kelimeleri, anahtar kelimeleri hesapla",
      img: "assets/image/home_2.jpg"
    },
    {
      title:"Website Benzerlik Sıralaması", 
      desc: "Girilen iki website arasında benzerliklerini kıyasla",
      img: "assets/image/home_3.jpg"
    },
    {
      title:"Alt URL'ler ile Benzerlik Sıralaması", 
      desc: "Girilen websitesi havuzu arasında alt url'leri dahil ederek benzerliklerini kıyasla",
      img: "assets/image/home_4.jpg"
    },
    {
      title:"Semantik Benzerlik Sıralaması", 
      desc: "Girilen websitesi havuzu arasında alt url'leri ve semantik kelimeleri dahil ederek benzerliklerini kıyasla",
      img: "assets/image/home_5.jpg"
    }
  ];





  ngOnInit(): void {
  }

}
