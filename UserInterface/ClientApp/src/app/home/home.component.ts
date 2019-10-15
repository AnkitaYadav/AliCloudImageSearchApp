
import { Component, OnInit } from '@angular/core';
import {HomeService } from '../Service/home.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls:['./home.component.css']
})
export class HomeComponent implements OnInit {
  imgURL: any;
  imagePath:any;
  ngOnInit(): void {
    this.getAllImages();
    }

  constructor(private homeService: HomeService) { }
  listImages: any[];
  getAllImages() {

    this.homeService.getAllImages().subscribe(response =>
    {
      debugger;
      this.listImages = response;
    });
  }
  private base64textString: String = "";

  handleFileSelect(evt) {
    debugger;
    var files = evt.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();
      //this.imagePath = files;
      //reader.readAsDataURL(files[0]); 
      reader.onload = this._handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    debugger;
    var binaryString = readerEvt.target.result;
    this.base64textString = btoa(binaryString);
    var binaryImageString = btoa(binaryString);
    this.imgURL = "data:image/png;base64," + binaryImageString;
    console.log(btoa(binaryString));
    this.filterImageData(binaryImageString);
  }

  filterImageData(binaryString:string) {
    debugger;
    let jsonData = JSON.stringify(binaryString);
    jsonData = jsonData.replace(/\+/gi, '%2B');
    this.homeService.uploadImage(binaryString).subscribe(response => {
      console.log("filtered Image")
      this.listImages = response;
    }
      );

  }

  public uploadFile = (files) => {
    debugger;
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.homeService.filterImages(formData).subscribe((response:any ) => {
      debugger;
      this.listImages = response;}
  
    );
  }
}
