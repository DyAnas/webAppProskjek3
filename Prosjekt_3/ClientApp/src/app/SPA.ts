import { Component, ChangeDetectorRef } from "@angular/core";
import { Spørsmål, ISpørsmål } from "./Spørsmål";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { type, Itype } from "./Typetsx";
import "rxjs/add/operator/map";

@Component({
    selector: "min-app",
    templateUrl: "app.component.html"



})



export class spa {
    VisType: boolean;
    laster: boolean;
    alleTyper: Array<type>;
    enType: type;
    VisBilSp: boolean;
    etSp: Spørsmål;
    alleSpørsmål: Array<Spørsmål>;
    etSv: any;
    visSvar: boolean;
    VisEnSp: boolean;
    visSkjema: boolean;
    ViRuSp: boolean;
    skjemaStatus: string;
    skjema: FormGroup;
    constructor(private _http: HttpClient, private fb: FormBuilder) {
        this.skjema = fb.group({
            Id: [""],
            spørmål: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zøæåA-ZØÆÅ. \\-]{2,200}$")])],
            svar: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zøæåA-ZØÆÅ. \\-]{2,200}$")])],
            rating: [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]$")])],
            stemmer: [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]$")])],
        });
    }
    ngOnInit() {
        this.laster = true;
        this.visSkjema = false;
        this.hentAlletyper();

    }


    hentAlletyper() {
        this._http.get<Itype[]>("api/type/")
            .subscribe(
                typer => {
                    this.alleTyper = typer;
                    this.laster = false;
                 
                },
                error => alert(error)
            );
    };

    hentalleSpørsmål(TypeId: number) {
        this._http.get<ISpørsmål[]>("api/Spørsmål/" + TypeId)
            .subscribe(
                sp => {
                    this.alleSpørsmål = sp;
                    this.laster = false;
                    if (TypeId == 1)
                        this.VisBilSp = true;
                    else if (TypeId == 2)
                        this.VisEnSp = true;
                    else if (TypeId == 3)
                        this.ViRuSp = true;
                        } 
            );  
    }

    hentSvar(Id: number) {
        this._http.get("api/Spørsmål/" + Id)
            .subscribe(
                sp => {
                    this.etSv.svar = sp;
                    this.visSvar = true;
                }
            );
    }



    visTypeView(TypeId: number) {
        this.laster = true;
        this.hentalleSpørsmål(TypeId);
    }

    // Billettsp
    visBilSpView(Id: number) {
        this.laster = true;
        
        this.hentSvar(Id);
    }
    vedSubmit() {
        if (this.skjemaStatus == "Registrere") {
            this.lagresp();
        }
      
        else {
            alert("Feil i applikasjonen!");
        }
    }

    lagresp() {
        var sp = new Spørsmål();
        sp.spørmål = this.skjema.value.spørmål;
        sp.rating = 0;
        sp.stemmer = 0;
        sp.svar = "";
        sp.TypeId = this.skjema.value.TypeId;

        const body: string = JSON.stringify(sp);
        const headers = new HttpHeaders({ "Content-Type": "application/json" })
        this._http.post("api/Spørsmål", body, { headers: headers })
            .subscribe(
                () => {
                    this.hentalleSpørsmål(sp.Id);
                    this.visSkjema = false;
                    this.VisType= true;
                    console.log("ferdig post-api/Spørsmål");
                },
                error => alert(error)
            );
    };
    tilbakeTilListe() {
        this.VisType = true;
        this.visSkjema = false;
    }


}

