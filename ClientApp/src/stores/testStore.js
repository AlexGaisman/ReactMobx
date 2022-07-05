import React from 'react';
import { computed, makeObservable, makeAutoObservable, observable, action } from 'mobx';

const TestStore = observable({
    pets :[],
    owners :[],

    get totalOwners() {
        return this.pets.length;
    },

    getPets() {
        return this.pets;
    }

});

export default TestStore;