import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { forEach } from '@angular/router/src/utils/collection';

@Injectable()
export class HomeService {

  constructor(private http: HttpClient) { }

  getAllImages(): Observable<any> {
    debugger;
    return this.http.get('http://localhost:56488/api/SampleData/GetOssImages');
  }

  filterImage(imageString: any) {
    debugger;

    return this.http.post('http://localhost:56488/api/SampleData/UploadProfilePicture/', imageString);
  }
  filterImages(imageString: any) {
    debugger;

    return this.http.post('http://localhost:56488/api/SampleData/UploadProfilePicture/', imageString);
  }

  UploadFile(fileToUpload: FileList, catgoryId :string): Observable<Object> {
    debugger;
    const endpoint = 'http://localhost:56488/api/SampleData/uploadfile/' + catgoryId;
    let formData: FormData = new FormData();
    //let file = fileToUpload.item[0];
    for (let i = 0; i < fileToUpload.length; i++) {
      formData.append('images', fileToUpload[i], fileToUpload[i].name);
      //text += cars[i] + "<br>";
    }
    return this.http.post(endpoint, formData);
  }


  uploadImage(imageString: any): Observable<any> {
    var headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    let params = new URLSearchParams()
    params.set('imageString', imageString);
  

    return this.http.post('http://localhost:56488/api/SampleData/FilterImage/', params.toString(), {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded;charset=utf-8')
    });
    }
}
