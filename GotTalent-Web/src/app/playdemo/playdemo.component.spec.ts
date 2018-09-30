/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { WebcamComponent } from 'ngx-webcam';
import { PlaydemoComponent } from './playdemo.component';

describe('PlaydemoComponent', () => {

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        PlaydemoComponent,
        WebcamComponent
      ],
      imports: [
        FormsModule
      ]
    })
    .compileComponents();
  }));

  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(PlaydemoComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
  it('should render title in a h1 tag', async(() => {
    const fixture = TestBed.createComponent(PlaydemoComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Play demo!');
  }));
});
