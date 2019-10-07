import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { FileUploader } from "ng2-file-upload";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import {HomeService } from '../Service/home.service';


@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent  {
  public currentCount = 0;
  fileToUpload: FileList = null;
  category: any = "-1";
  constructor(private _homeService: HomeService)
  { }

  handleFileInput(files: FileList) {
    debugger;
    if (files) {
    }
    this.fileToUpload = files;
    this.uploadFileToActivity();
  }
  uploadFileToActivity() {
    debugger;
    this._homeService.UploadFile(this.fileToUpload, this.category).subscribe(data => {
      // do something, if upload success
      debugger;
      alert(data);
    }, error => {
      console.log(error);
    });
  }
  onChange(categoryId) {
    this.category = categoryId;
  }
  
}
