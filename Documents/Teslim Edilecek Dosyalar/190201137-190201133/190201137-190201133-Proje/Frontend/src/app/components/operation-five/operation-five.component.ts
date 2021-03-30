import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { WebSiteBasic } from 'src/app/models/websiteBasic/WebSiteBasic';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { OperationFourModel } from 'src/app/models/operation-four/operationFourModel';
import { NzFormatEmitEvent } from 'ng-zorro-antd/tree';
import { OperationFiveService } from 'src/app/services/operationFive/operation-five.service';
import { OperationFiveModel } from 'src/app/models/operation-five/operationFiveModel';

@Component({
  selector: 'app-operation-five',
  templateUrl: './operation-five.component.html',
  styleUrls: ['./operation-five.component.css']
})
export class OperationFiveComponent implements OnInit {

  constructor(
    private operationFiveService: OperationFiveService,
    private notificationService: NzNotificationService

  ) { }

  @ViewChild(TemplateRef, { static: false }) template?: TemplateRef<{}>;
  webSite: string;
  sendUrl: string = "";
  urlList: string[] = [];
  webSitePool: WebSiteBasic[] = [];
  result: OperationFiveModel;
  control = false;
  send = false;
  ngOnInit(): void {}

  nzEvent(event: NzFormatEmitEvent): void {
    console.log(event);
  }


  closeLoading() {
    this.control = false;
  }

  addUrl() {
    var control = false;
    if (this.sendUrl != ' ' && this.sendUrl != '') {
      this.urlList.forEach((p) => {
        if (p === this.sendUrl) control = true;
      });
      if (!control) {
        this.urlList.push(this.sendUrl);
        this.sendUrl = '';
        this.notificationService.template(this.template!, {
          nzData: {
            title: 'Harika!!',
            message: 'Url başarıyla havuza eklendi.',
            color: 'green',
          },
        });
      } else {
        this.notificationService.template(this.template!, {
          nzData: {
            title: 'Hata!!',
            message: 'Url hatalı veya havuzda bulunuyor.',
            color: 'red',
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

  removeUrl(index: number) {
    this.urlList.splice(index, 1);
    this.notificationService.template(this.template!, {
      nzData: {
        title: 'Harika!!',
        message: 'Url başarıyla havuzdan kaldırıldı.',
        color: 'green',
      },
    });
  }

  getSimilarity() {
    if (
      this.webSite != null &&
      this.urlList != null &&
      this.urlList.length > 0
    ) {
      this.send = true;

      this.urlList.forEach((item) => {
        var webSite: WebSiteBasic = {
          url: item,
        };
        this.webSitePool.push(webSite);
      });
      this.operationFiveService
        .getSimilarity(this.webSite, this.webSitePool)
        .subscribe((response) => {
          console.log(response)
          this.result = response.data;
          this.send = false;
          this.webSitePool = [];
          
        });
    } else {
      this.notificationService.template(this.template!, {
        nzData: {
          title: 'Hata!!',
          message: '1. Website veya havuz boş',
          color: 'red',
        },
      });
    }
  }

}
