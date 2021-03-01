# 安装lme4包
options(repos=structure( c(CRAN="https://mirrors.tuna.tsinghua.edu.cn/CRAN/")))

library(lme4)

# 样本数据
inputfile <- "Data/TestData_lme4.csv"
testdata <- read.csv(inputfile, encoding="UTF-8")
print(testdata)
len <- testdata$len #齿长
supp <- testdata$supp #给予方式
dose <- testdata$dose #剂量

# 提交给 lmer() 函数
relation  = lmer(len ~ dose + (1|supp), data = testdata) 

#模型结果输出
summary(relation) 

#模型固定因子方差分析
anova(relation)