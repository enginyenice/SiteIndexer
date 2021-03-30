import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { OperationOneModel } from 'src/app/models/operation-one/operationOneModel';
import { OperationOneService } from 'src/app/services/operationOne/operation-one.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
@Component({
  selector: 'app-operation-one',
  templateUrl: './operation-one.component.html',
  styleUrls: ['./operation-one.component.css']
})
export class OperationOneComponent implements OnInit {
  @ViewChild(TemplateRef, { static: false }) template?: TemplateRef<{}>;
  
  constructor(private operationOneService:OperationOneService,    private notificationService: NzNotificationService) { }

  ngOnInit(): void {
  }
  
  
  operationOneData: OperationOneModel;
  dataLoaded = false;
  send = false;
  getFrequency(url:String){
    if(url != "" && url.length != 0 && url != null){
      this.send = true;
    this.operationOneService.getFrequency(url).subscribe(response => {
      this.operationOneData = response.data;
      this.dataLoaded = true;
      this.send = false;
    })
    } else {
      this.notificationService.template(this.template!, {
        nzData: {
          title: 'Hata!!',
          message: 'Url hatalı. Tekrar kontrol ediniz.',
          color: 'red',
        },
      });
    }
  }


}
