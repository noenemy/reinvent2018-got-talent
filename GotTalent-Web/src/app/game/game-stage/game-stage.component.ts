import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { WebcamImage, WebcamInitError, WebcamUtil } from 'ngx-webcam';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Headers, Response, RequestOptions } from '@angular/http';
import { AlertifyService } from '../../_services/alertify.service';
import { StagelogService } from '../../_services/stagelog.service';
import { StageLog } from '../../_models/stagelog';

@Component({
  selector: 'app-game-stage',
  templateUrl: './game-stage.component.html',
  styleUrls: ['./game-stage.component.css']
})
export class GameStageComponent implements OnInit {
  @Input() stage: string;
  @Input() game_id: number;
  @Input() action_type: string;
  @Output() stageCompleted = new EventEmitter<string>();

  // toggle webcam on/off
  public showWebcam = true;
  public allowCameraSwitch = true;
  public multipleWebcamsAvailable = false;
  public deviceId: string;
  public videoOptions: MediaTrackConstraints = {
    // width: {ideal: 1024},
    // height: {ideal: 576}
  };
  public errors: WebcamInitError[] = [];

  constructor(private http: HttpClient,
    private stagelogService: StagelogService,
    private alertify: AlertifyService) { }

  // latest snapshot
  public webcamImage: WebcamImage = null;

  // webcam snapshot trigger
  private trigger: Subject<void> = new Subject<void>();
  // switch to next / previous / specific webcam; true/false: forward/backwards, string: deviceId
  private nextWebcam: Subject<boolean|string> = new Subject<boolean|string>();

  ngOnInit() {
    WebcamUtil.getAvailableVideoInputs()
      .then((mediaDevices: MediaDeviceInfo[]) => {
        this.multipleWebcamsAvailable = mediaDevices && mediaDevices.length > 1;
      });
  }

  public triggerSnapshot(): void {
    this.trigger.next();
    this.postStageLog();
  }

  public toggleWebcam(): void {
    this.showWebcam = !this.showWebcam;
  }

  public handleInitError(error: WebcamInitError): void {
    this.errors.push(error);
  }

  public showNextWebcam(directionOrDeviceId: boolean|string): void {
    // true => move forward through devices
    // false => move backwards through devices
    // string => move to device with given deviceId
    this.nextWebcam.next(directionOrDeviceId);
  }

  public handleImage(webcamImage: WebcamImage): void {
    console.log('received webcam image', webcamImage);
    this.webcamImage = webcamImage;
  }

  public cameraWasSwitched(deviceId: string): void {
    console.log('active device: ' + deviceId);
    this.deviceId = deviceId;
  }

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  public get nextWebcamObservable(): Observable<boolean|string> {
    return this.nextWebcam.asObservable();
  }

  public postStageLog() {

    const stageLog = {
      gameId: this.game_id,
      actionType: this.action_type,
      base64Image: this.webcamImage.imageAsBase64
    };

    this.stagelogService.addStageLog(stageLog).subscribe((stageLogResult: StageLog) => {

      this.alertify.success('Successfully uploaded!');

      if (stageLogResult.action_type === 'Profile') {
        this.alertify.message('age:' + stageLogResult.age + ', gender:' + stageLogResult.gender);
      } else {
        this.alertify.message(stageLogResult.action_type + ':' + stageLogResult.score);
      }

      this.stageCompleted.emit(this.action_type);
    }, error => {
      this.alertify.error(error);
    });
  }
}

