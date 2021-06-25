clear
clc
close all
cover = imread('C:/VideoSteganography/allFrames/000129.png');
stego = imread('C:/VideoSteganography/stegoFrameStore/000129.png');

figure;
imhist(rgb2gray(cover));
figure;
imhist(rgb2gray(stego));