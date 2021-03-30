import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { OperationThreeModel } from 'src/app/models/operation-three/operationThreeModel';
import { OperationThreeService } from 'src/app/services/operationThree/operation-three.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { WebSiteBasic } from 'src/app/models/websiteBasic/WebSiteBasic';

@Component({
  selector: 'app-operation-three',
  templateUrl: './operation-three.component.html',
  styleUrls: ['./operation-three.component.css']
})
export class OperationThreeComponent implements OnInit {

  constructor(private operationThreeService:OperationThreeService,private notificationService: NzNotificationService) { }
  @ViewChild(TemplateRef, { static: false }) template?: TemplateRef<{}>;
  webSite:string;
  sendUrl:string = "";
  urlList:string[] = [];
  webSitePool:WebSiteBasic[] = [];
  result : OperationThreeModel;
  control = false;
  send = false;
  ngOnInit(): void {
  }
 
  closeLoading(){
    this.control=false;
  }


  addUrl(){
    var control = false;
    if (this.sendUrl != " " && this.sendUrl!= ""){
      this.urlList.forEach(p=> {if(p === this.sendUrl) control=true;});
      if (!control){
        this.urlList.push(this.sendUrl); 
        this.sendUrl = "";
        this.notificationService.template(this.template!, { 
          nzData: { 
            title: 'Harika!!', 
            message: 'Url başarıyla havuza eklendi.', 
            color: 'green' 
          },
        });
      } 
      else {
        this.notificationService.template(this.template!, { 
          nzData: { 
            title: 'Hata!!', 
            message: 'Url hatalı veya havuzda bulunuyor.', 
            color: 'red' 
          }, 
        });
      }
    } 
    else{
      this.notificationService.template(this.template!, {
        nzData: {
          title: 'Hata!!',
          message: 'Url alanı boş.',
          color: 'red',
        },
      });
    }       
  }

  removeUrl(index:number){
    this.urlList.splice(index,1);
    this.notificationService.template(this.template!, { nzData: { title: 'Harika!!', message: 'Url başarıyla havuzdan kaldırıldı.', color: 'green' } });
  }


  


  getSimilarity(){
    
    if(this.webSite != null && this.urlList != null && this.urlList.length > 0){
      this.send = true;
      
      this.urlList.forEach(item => {
        var webSite:WebSiteBasic = {
          url:item
        };
        this.webSitePool.push(webSite);
      });
      this.operationThreeService.getSimilarity(this.webSite,this.webSitePool).subscribe(response => {
          this.result = response.data;
          this.send = false;
          this.webSitePool = [];
        }
        );
    }
     else {
      this.notificationService.template(this.template!, { nzData: { title: 'Hata!!', message: '1. Website veya havuz boş', color: 'red' } });
    }
   
  }
}
