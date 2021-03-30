import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { WebSiteBasic } from 'src/app/models/websiteBasic/WebSiteBasic';
import { OperationFourService } from 'src/app/services/operationFour/operation-four.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { OperationFourModel } from 'src/app/models/operation-four/operationFourModel';
import { NzFormatEmitEvent } from 'ng-zorro-antd/tree';

@Component({
  selector: 'app-operation-four',
  templateUrl: './operation-four.component.html',
  styleUrls: ['./operation-four.component.css'],
})
export class OperationFourComponent implements OnInit {
  constructor(
    private operationFourService: OperationFourService,
    private notificationService: NzNotificationService
  ) {}
  @ViewChild(TemplateRef, { static: false }) template?: TemplateRef<{}>;
  webSite: string;
  sendUrl: string = '';
  urlList: string[] = [];
  webSitePool: WebSiteBasic[] = [];
  result: OperationFourModel;
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
    } else {
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
      this.operationFourService
        .getSimilarity(this.webSite, this.webSitePool)
        .subscribe((response) => {
          this.result = response.data;
          this.send = false;
          this.webSitePool = [];
          console.log(response);
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
