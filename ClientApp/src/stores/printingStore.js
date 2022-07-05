import React from 'react';
import { computed, makeObservable, makeAutoObservable, observable, action } from 'mobx';

const printingStore = observable({
    user :'',
    owners :[],

    get totalOwners() {
        return this.pets.length;
    },

    getPets() {
        return this.pets;
    }

});

export default printingStore;