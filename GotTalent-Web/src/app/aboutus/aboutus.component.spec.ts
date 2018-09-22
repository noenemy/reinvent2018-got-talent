/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AboutusComponent } from './aboutus.component';

describe('AboutusComponent', () => {
  let component: AboutusComponent;
  let fixture: ComponentFixture<AboutusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AboutusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AboutusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
